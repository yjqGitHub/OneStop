using StackExchange.Redis;
using System;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：ConnectionMulitiplexerFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ConnectionMultiplexer工厂
    /// 创建标识：yjq 2017/4/28 13:56:26
    /// </summary>
    public sealed class RedisDatabaseProvider : JQDisposable, IRedisDatabaseProvider
    {
        private readonly RedisCacheOptions _redisCacheOptions;
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        public RedisDatabaseProvider(RedisCacheOptions redisCacheOptions)
        {
            _redisCacheOptions = redisCacheOptions;
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }

        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase(_redisCacheOptions.DatabaseId);
        }

        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            LogUtil.Info("执行redis链接打开方法");
            return ConnectionMultiplexer.Connect(_redisCacheOptions.ConnectionString);
        }

        protected override void DisposeCode()
        {
            LogUtil.Info("执行redis链接释放方法");
            _connectionMultiplexer.Value?.Dispose();
        }
    }
}