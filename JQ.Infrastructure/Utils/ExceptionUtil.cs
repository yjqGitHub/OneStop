using System;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：ExceptionUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：异常工具类
    /// 创建标识：yjq 2017/4/28 15:58:22
    /// </summary>
    public static class ExceptionUtil
    {
        #region 忽略异常，但记录异常

        /// <summary>
        /// 忽略异常，但记录异常
        /// </summary>
        /// <param name="action">执行的方法</param>
        /// <param name="memberName">调用成员信息</param>
        public static void LogException(Action action, string memberName = null)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName);
            }
        }

        /// <summary>
        /// 忽略异常，但记录异常
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="action">执行的方法</param>
        /// <param name="defaultValue">默认返回值</param>
        /// <param name="memberName">调用成员信息</param>
        /// <param name="defaultLoggerName">默认的日志文件名字</param>
        /// <returns>如果没异常，返回值就是正常返回值，假如出现了异常，返回值就是默认的值</returns>
        public static T LogException<T>(Func<T> action, T defaultValue = default(T), string memberName = null)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName);
                return defaultValue;
            }
        }

        #endregion 忽略异常，但记录异常
    }
}