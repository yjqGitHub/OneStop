using JQ.Infrastructure.Extension;
using StackExchange.Redis;
using System;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：DefaultRedisSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：默认redis序列化类
    /// 创建标识：yjq 2017/4/28 16:33:57
    /// </summary>
    public class DefaultRedisSerializer : IRedisSerializer
    {
        public virtual object Deserialize(RedisValue objbyte)
        {
            return Deserialize<object>(objbyte);
        }

        public virtual T Deserialize<T>(RedisValue objbyte)
        {
            string objectStr = objbyte;
            return objectStr.ToObjInfo<T>();
        }

        public virtual string Serialize(object value)
        {
            return Serialize<object>(value);
        }

        public virtual string Serialize(object value, Type type)
        {
            return Serialize<object>(value, type);
        }

        public virtual string Serialize<T>(T value)
        {
            return Serialize(value, value.GetType());
        }

        public virtual string Serialize<T>(T value, Type type)
        {
            return value.ToJson();
        }
    }
}