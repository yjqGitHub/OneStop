using JQ.Infrastructure.Extension;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
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
        private IDatabase _database;

        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        private readonly RedisCacheOptions _redisCacheOption;

        private ConnectionMultiplexer Connection { get { return _connectionMultiplexer.Value; } }

        public IDatabase Database
        {
            get { return _database ?? (_database = Connection.GetDatabase(_redisCacheOption.DatabaseId)); }
        }

        public StackExchangeRedisClient(RedisCacheOptions redisCacheOption)
        {
            _redisCacheOption = redisCacheOption;
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }

        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            return ConnectionMultiplexer.Connect(_redisCacheOption.ConnectionString);
        }

        private string SetPrefix(string key)
        {
            return _redisCacheOption.Prefix.IsNullOrEmptyWhiteSpace() ? key : $"{_redisCacheOption.Prefix}{_redisCacheOption.NamespaceSplitSymbol}{key}";
        }

        /// <summary>
        /// 清楚当前db的所有数据
        /// </summary>
        public void Clear()
        {
            DeleteKeyWithKeyPrefix("*");
        }

        /// <summary>
        /// 查找当前命名前缀下共有多少个Key
        /// </summary>
        /// <returns></returns>
        public int KeyCount()
        {
            return CalcuteKeyCount("*");
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
        /// <summary>
        /// 判断是否存在当前的Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return Database.KeyExists(SetPrefix(key));
        }
        /// <summary>
        /// 判断是否存在当前的Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> ExistsAsync(string key)
        {
            return Database.KeyExistsAsync(SetPrefix(key));
        }
        /// <summary>
        /// 移除当前key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return Database.KeyDelete(SetPrefix(key));
        }
        /// <summary>
        /// 移除当前key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> RemoveAsync(string key)
        {
            return Database.KeyDeleteAsync(SetPrefix(key));
        }
        /// <summary>
        /// 移除全部key
        /// </summary>
        /// <param name="keys"></param>
        public void RemoveAll(IEnumerable<string> keys)
        {
            keys.ForEach(key =>
            {
                Remove(key);
            });
        }
        /// <summary>
        /// 移除全部key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public Task RemoveAllAsync(IEnumerable<string> keys)
        {
            return keys.ForEachAsync(RemoveAsync);
        }

        //public T Get<T>(string key)
        //{
        //    var 
        //}

        /// <summary>
        /// 计算当前prefix开头的key总数
        /// </summary>
        /// <param name="prefix">key前缀</param>
        /// <returns></returns>
        private int CalcuteKeyCount(string prefix)
        {
            if (Database.IsNull())
            {
                return 0;
            }
            var retVal = Database.ScriptEvaluate("return table.getn(redis.call('keys', ARGV[1]))", values: new RedisValue[] { SetPrefix(prefix) });
            if (retVal.IsNull)
            {
                return 0;
            }
            return (int)retVal;
        }

        /// <summary>
        /// 删除以当前prefix开头的所有key缓存
        /// </summary>
        /// <param name="prefix">key前缀</param>
        private void DeleteKeyWithKeyPrefix(string prefix)
        {
            if (Database.IsNotNull())
            {
                Database.ScriptEvaluate(@"
                local keys = redis.call('keys', ARGV[1])
                for i=1,#keys,5000 do
                redis.call('del', unpack(keys, i, math.min(i+4999, #keys)))
                end", values: new RedisValue[] { SetPrefix(prefix) });
            }
        }
    }
}