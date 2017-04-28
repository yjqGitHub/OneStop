using JQ.Infrastructure.Extension;
using System.Xml;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：ConfigUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：app配置工具类
    /// 创建标识：yjq 2017/4/28 15:55:13
    /// </summary>
    public static class ConfigUtil
    {
        #region 根据Key获取app配置文件的设置的值

        /// <summary>
        /// 根据Key获取app配置文件的设置的值
        /// </summary>
        /// <param name="key">获取的Key</param>
        /// <param name="memberName">调用成员</param>
        /// <param name="defaultLoggerName">默认日志记录名</param>
        /// <returns>app配置文件的设置的值</returns>
        public static string GetValue(string key, string memberName = null)
        {
            return ExceptionUtil.LogException(() =>
            {
                #region 如果不启动监听，将这段代码去除即可

                string value = ConfigWacherUtil.GetValue(key);
                if (value.IsNotNullAndEmptyWhiteSpace())
                {
                    return value;
                }

                #endregion 如果不启动监听，将这段代码去除即可

                XmlDocument xmlDoc = LoadAppXml();
                var xmlNode = xmlDoc.SelectSingleNode("//appSettings");
                if (xmlNode != null)
                {
                    XmlElement xmlElement = xmlNode.SelectSingleNode("//add[@key='" + key + "']") as XmlElement;
                    if (xmlElement != null)
                    {
                        return xmlElement.GetAttribute("value");
                    }
                }
                return null;
            }, memberName: memberName);
        }

        #endregion 根据Key获取app配置文件的设置的值

        #region 根据Key设置app配置文件的值，没有则添加

        /// <summary>
        /// 根据Key设置app配置文件的值，没有则添加
        /// </summary>
        /// <param name="key">要设置的Key</param>
        /// <param name="value">设置的值</param>
        /// <param name="memberName">调用成员</param>
        /// <param name="defaultLoggerName">默认日志记录名</param>
        public static void SetValue(string key, string value, string memberName = null)
        {
            ExceptionUtil.LogException(() =>
            {
                XmlDocument xDoc = LoadAppXml();
                XmlNode xNode;
                XmlElement xElem1;
                XmlElement xElem2;
                xNode = xDoc.SelectSingleNode("//appSettings");
                xElem1 = xNode.SelectSingleNode("//add[@key='" + key + "']") as XmlElement;
                if (xElem1 != null)
                {
                    xElem1.SetAttribute("value", value);
                }
                else
                {
                    xElem2 = xDoc.CreateElement("add");
                    xElem2.SetAttribute("key", key);
                    xElem2.SetAttribute("value", value);
                    xNode.AppendChild(xElem2);
                }
                xDoc.Save(JQConfiguration.AppConfigPath);
            }, memberName: memberName);
        }

        #endregion 根据Key设置app配置文件的值，没有则添加

        #region 加载App配置文件

        /// <summary>
        /// 加载App配置文件
        /// </summary>
        /// <returns>App配置文件</returns>
        private static XmlDocument LoadAppXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (JQConfiguration.AppConfigPath.IsNullOrEmptyWhiteSpace() || FileUtil.IsNotExistsFile(JQConfiguration.AppConfigPath))
            {
                return xmlDoc;
            }
            xmlDoc.Load(JQConfiguration.AppConfigPath);
            return xmlDoc;
        }

        #endregion 加载App配置文件
    }
}