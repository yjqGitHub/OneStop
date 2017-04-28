using StackExchange.Redis;
using System;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：IRedisDatabaseProvider.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：RedisDatabase工厂
    /// 创建标识：yjq 2017/4/28 16:20:29
    /// </summary>
    public interface IRedisDatabaseProvider : IDisposable
    {
        IDatabase GetDatabase();
    }
}