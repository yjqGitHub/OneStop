using JQ.Infrastructure.Extension;
using System;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：LogUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：日志记录工具类
    /// 创建标识：yjq 2017/4/28 10:49:34
    /// </summary>
    public static class LogUtil
    {
        private static ILoggerFactory GetLoggerFactory()
        {
            return ObjectContainer.Resolve<ILoggerFactory>();
        }

        /// <summary>
        /// 获取日志记录器
        /// </summary>
        /// <param name="loggerName">记录器名字</param>
        /// <returns>日志记录器</returns>
        public static ILogger GetLogger(string loggerName = null, Type type = null)
        {
            if (!string.IsNullOrWhiteSpace(loggerName))
            {
                return GetLoggerFactory().Create(loggerName);
            }
            if (type != null)
            {
                return GetLoggerFactory().Create(type);
            }
            return GetLoggerFactory().Create(JQConfiguration.DefaultLoggerName);
        }

        /// <summary>
        /// 输出调试日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Debug(string msg, string loggerName = null, Type type = null)
        {
            GetLogger(loggerName: loggerName, type: type).Debug(msg);
        }

        /// <summary>
        /// 输出普通日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Info(string msg, string loggerName = null, Type type = null)
        {
            GetLogger(loggerName: loggerName, type: type).Info(msg);
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="loggerName"></param>
        public static void Warn(string msg, string loggerName = null, Type type = null)
        {
            GetLogger(loggerName: loggerName, type: type).Warn(msg);
        }

        /// <summary>
        /// 输出警告日志信息
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="memberName"></param>
        /// <param name="loggerName"></param>
        public static void Warn(Exception ex, string memberName = null, string loggerName = null, Type type = null)
        {
            GetLogger(loggerName: loggerName, type: type).Warn(ex.ToErrMsg(memberName: memberName));
        }

        /// <summary>
        /// 输出错误日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Error(string msg, string loggerName = null, Type type = null)
        {
            GetLogger(loggerName: loggerName, type: type).Error(msg);
        }

        /// <summary>
        /// 输出错误日志信息
        /// </summary>
        /// <param name="ex">异常信息</param>
        public static void Error(Exception ex, string memberName = null, string loggerName = null, Type type = null)
        {
            GetLogger(loggerName: loggerName, type: type).Error(ex.ToErrMsg(memberName: memberName));
        }
    }
}