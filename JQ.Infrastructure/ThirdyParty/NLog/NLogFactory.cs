using System;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：NLogFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：NLogger记录器创建工厂
    /// 创建标识：yjq 2017/4/28 10:46:53
    /// </summary>
    public sealed class NLogFactory : ILoggerFactory
    {
        /// <summary>
        /// 根据名字创建ILogger
        /// </summary>
        /// <param name="name">logger名字</param>
        /// <returns>ILogger</returns>
        public ILogger Create(string name)
        {
            return new NLogLogger(NLog.LogManager.GetLogger(name));
        }

        /// <summary>
        /// 根据类型创建一个ILogger
        /// </summary>
        /// <param name="type">logger类型</param>
        public ILogger Create(Type type)
        {
            return new NLogLogger(NLog.LogManager.GetLogger(type.Name, type));
        }
    }
}