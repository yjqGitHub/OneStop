using System;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：ILoggerFactory.cs
    /// 类属性：公共接口（非静态）
    /// 类功能描述：创建日志记录器工厂
    /// 创建标识：yjq 2017/4/28 10:45:10
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// 根据名字创建ILogger
        /// </summary>
        /// <param name="name">logger名字</param>
        /// <returns>ILogger</returns>
        ILogger Create(string name);

        /// <summary>
        /// 根据类型创建一个ILogger
        /// </summary>
        /// <param name="type">logger类型</param>
        ILogger Create(Type type);
    }
}