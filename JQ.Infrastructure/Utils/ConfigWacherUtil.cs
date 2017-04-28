using JQ.Infrastructure.Extension;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Xml;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：ConfigWacherUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：配置文件监听
    /// 创建标识：yjq 2017/4/28 15:51:00
    /// </summary>
    public static class ConfigWacherUtil
    {
        private static Timer _timer;
        private static FileInfo _configInfo;
        private static FileSystemWatcher _watcher;
        private static int _timeoutMillis = 500;
        private static ConcurrentDictionary<string, string> _configCache = new ConcurrentDictionary<string, string>();//配置文件的缓存

        /// <summary>
        /// 启动监听
        /// </summary>
        /// <param name="configFilePath">appconfig文件路径</param>
        public static void Install()
        {
            if (FileUtil.IsExistsFile(JQConfiguration.AppConfigPath))
            {
                _configInfo = new FileInfo(JQConfiguration.AppConfigPath);
                InternalConfigure(_configInfo);
                _watcher = new FileSystemWatcher();
                _watcher.Path = _configInfo.DirectoryName;
                _watcher.Filter = _configInfo.Name;
                _watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.FileName;
                _watcher.Changed += new FileSystemEventHandler(ConfigWacherHandler_OnChanged);
                _watcher.Created += new FileSystemEventHandler(ConfigWacherHandler_OnChanged);
                _watcher.Deleted += new FileSystemEventHandler(ConfigWacherHandler_OnChanged);
                _watcher.Renamed += new RenamedEventHandler(ConfigWacherHandler_OnRenamed);

                _watcher.EnableRaisingEvents = true;

                _timer = new Timer(new TimerCallback(OnWatchedFileChange), null, Timeout.Infinite, Timeout.Infinite);
                LogUtil.Info("文件监听器开启");
            }
            else
            {
                LogUtil.Error(string.Format("【{0}】配置文件不存在", JQConfiguration.AppConfigPath));
            }
        }

        /// <summary>
        /// 获取配置文件的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            if (key.IsNullOrEmptyWhiteSpace()) return null;
            string value = null;
            if (_configCache.TryGetValue(key, out value))
            {
                return value;
            }
            return null;
        }

        private static void ConfigWacherHandler_OnChanged(object source, FileSystemEventArgs e)
        {
            _timer.Change(_timeoutMillis, Timeout.Infinite);
        }

        private static void ConfigWacherHandler_OnRenamed(object source, RenamedEventArgs e)
        {
            _timer.Change(_timeoutMillis, Timeout.Infinite);
        }

        private static void OnWatchedFileChange(object state)
        {
            InternalConfigure(_configInfo);
        }

        private static void InternalConfigure(FileInfo configFile)
        {
            if (configFile == null)
            {
                LogUtil.Error("【InternalConfigure】配置文件不存在");
            }
            else
            {
                if (File.Exists(configFile.FullName))
                {
                    FileStream fs = null;
                    for (int retry = 5; --retry >= 0;)
                    {
                        try
                        {
                            fs = configFile.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
                            break;
                        }
                        catch (IOException ex)
                        {
                            if (retry == 0)
                            {
                                LogUtil.Error(string.Format("Failed to open XML config file [{0}],{1}", configFile.FullName, ex.ToErrMsg("InternalConfigure")));
                                fs = null;
                            }
                            Thread.Sleep(250);
                        }
                    }

                    if (fs != null)
                    {
                        try
                        {
                            InternalConfigure(fs);
                            LogUtil.Info("配置文件信息加载完毕");
                        }
                        finally
                        {
                            fs.Close();
                        }
                    }
                }
                else
                {
                    LogUtil.Error(string.Format("【InternalConfigure{0}】配置文件不存在", configFile.FullName));
                }
            }
        }

        private static void InternalConfigure(FileStream configStream)
        {
            if (configStream == null)
            {
                LogUtil.Error("【configStream】为空");
            }
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(configStream);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.ToErrMsg("InternalConfigure"));
                doc = null;
            }

            if (doc != null)
            {
                XmlNode xNode = doc.SelectSingleNode("//appSettings");
                if (xNode != null && xNode.HasChildNodes)
                {
                    string key = string.Empty;
                    string value = string.Empty;
                    for (int i = 0; i < xNode.ChildNodes.Count; i++)
                    {
                        XmlElement childrenElement = xNode.ChildNodes[i] as XmlElement;
                        if (childrenElement != null)
                        {
                            key = childrenElement.GetAttribute("key");
                            value = childrenElement.GetAttribute("value");
                            if (key.IsNotNullAndEmptyWhiteSpace())
                            {
                                _configCache[key] = value;
                            }
                        }
                    }
                }
            }
        }

        public static void Close()
        {
            _watcher?.Dispose();
            _timer?.Dispose();
            LogUtil.Info("释放文件监听器");
        }
    }
}