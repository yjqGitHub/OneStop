using JQ.Infrastructure.Extension;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private readonly ISerializer _serializer;

        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        private readonly RedisCacheOptions _redisCacheOption;

        private ConnectionMultiplexer Connection { get { return _connectionMultiplexer.Value; } }

        public IDatabase Database
        {
            get { return _database ?? (_database = Connection.GetDatabase(_redisCacheOption.DatabaseId)); }
        }

        public ISerializer Serializer { get { return _serializer; } }

        public StackExchangeRedisClient(RedisCacheOptions redisCacheOption, ISerializer serializer)
        {
            _redisCacheOption = redisCacheOption;
            _serializer = serializer;
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

        #region Keys

        /// <summary>
        /// 查找当前命名前缀下共有多少个Key
        /// </summary>
        /// <returns></returns>
        public int KeyCount()
        {
            return CalcuteKeyCount("*");
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            Connection?.Dispose();
        }

        /// <summary>
        /// 查找键名
        /// </summary>
        /// <param name="pattern">匹配项</param>
        /// <returns>匹配上的所有键名</returns>
        public IEnumerable<string> SearchKeys(string pattern)
        {
            var endpoints = Connection?.GetEndPoints();

            if (endpoints == null || !endpoints.Any() || Connection == null) return null;

            return Connection.GetServer(endpoints.First())
                .Keys(_redisCacheOption.DatabaseId, pattern)
                .Select(r => (string)r);
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
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Expire(string key, TimeSpan expiry)
        {
            return Database.KeyExpire(SetPrefix(key), expiry);
        }

        /// <summary>
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> ExpireAsync(string key, TimeSpan expiry)
        {
            return Database.KeyExpireAsync(SetPrefix(key), expiry);
        }

        /// <summary>
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Expire(string key, DateTime expiry)
        {
            return Database.KeyExpire(SetPrefix(key), expiry);
        }

        /// <summary>
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> ExpireAsync(string key, DateTime expiry)
        {
            return Database.KeyExpireAsync(SetPrefix(key), expiry);
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

        #endregion Keys

        #region StringSet

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        public bool StringSet<T>(string key, T value)
        {
            var objBytes = Serializer.Serialize(value);
            return Database.StringSet(SetPrefix(key), objBytes);
        }

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        public async Task<bool> StringSetAsync<T>(string key, T value)
        {
            var objBytes = await Serializer.SerializeAsync(value);
            return await Database.StringSetAsync(SetPrefix(key), objBytes);
        }

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresIn">过期间隔</param>
        /// <returns>成功返回true</returns>
        public bool StringSet<T>(string key, T value, TimeSpan expiresIn)
        {
            var objBytes = Serializer.Serialize(value);
            return Database.StringSet(SetPrefix(key), objBytes, expiresIn);
        }

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresIn">过期间隔</param>
        /// <returns>成功返回true</returns>
        public async Task<bool> StringSetAsync<T>(string key, T value, TimeSpan expiresIn)
        {
            var objBytes = await Serializer.SerializeAsync(value);
            return await Database.StringSetAsync(SetPrefix(key), objBytes, expiresIn);
        }

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">过期时间</param>
        /// <returns>成功返回true</returns>
        public bool StringSet<T>(string key, T value, DateTimeOffset expiresAt)
        {
            var objBytes = Serializer.Serialize(value);
            var expiration = expiresAt.Subtract(DateTimeOffset.Now);
            return Database.StringSet(SetPrefix(key), objBytes, expiration);
        }

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">过期时间</param>
        /// <returns>成功返回true</returns>
        public async Task<bool> StringSetAsync<T>(string key, T value, DateTimeOffset expiresAt)
        {
            var objBytes = await Serializer.SerializeAsync(value);
            var expiration = expiresAt.Subtract(DateTimeOffset.Now);
            return await Database.StringSetAsync(SetPrefix(key), objBytes, expiration);
        }

        /// <summary>
        /// 批量设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="items">键值列表</param>
        /// <returns>成功返回true</returns>
        public bool StringSetAll<T>(IList<Tuple<string, T>> items)
        {
            var values = items.Select(m => new KeyValuePair<RedisKey, RedisValue>(SetPrefix(m.Item1), Serializer.Serialize(m.Item2))).ToArray();
            return Database.StringSet(values);
        }

        /// <summary>
        /// 批量设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="items">键值列表</param>
        /// <returns>成功返回true</returns>
        public async Task<bool> StringSetAllAsync<T>(IList<Tuple<string, T>> items)
        {
            var values = items.Select(m => new KeyValuePair<RedisKey, RedisValue>(SetPrefix(m.Item1), Serializer.Serialize(m.Item2))).ToArray();
            return await Database.StringSetAsync(values);
        }

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        public long StringIncrement(string key, long value = 1)
        {
            return Database.StringIncrement(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        public Task<long> StringIncrementAsync(string key, long value = 1)
        {
            return Database.StringIncrementAsync(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        public double StringIncrementDouble(string key, double value)
        {
            return Database.StringIncrement(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        public Task<double> StringIncrementDoubleAsync(string key, double value)
        {
            return Database.StringIncrementAsync(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        public long StringDecrement(string key, long value = 1)
        {
            return Database.StringDecrement(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        public Task<long> StringDecrementAsync(string key, long value = 1)
        {
            return Database.StringDecrementAsync(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        public double StringDecrementDouble(string key, double value)
        {
            return Database.StringDecrement(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        public Task<double> StringDecrementDoubleAsync(string key, double value)
        {
            return Database.StringDecrementAsync(SetPrefix(key), value);
        }

        #endregion StringSet

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