using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：StackExchangeRedisClient.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：StackExchangeRedis实例
    /// 创建标识：yjq 2017/4/28 11:43:29
    /// </summary>
    public sealed class StackExchangeRedisClient : IRedisClient
    {
        private readonly IDatabase _database;
        private readonly IRedisSerializer _serializer;

        public StackExchangeRedisClient(IRedisDatabaseProvider databaseProvider, IRedisSerializer serializer)
        {
            _database = databaseProvider.GetDatabase();
            _serializer = serializer;
        }
    }
}
