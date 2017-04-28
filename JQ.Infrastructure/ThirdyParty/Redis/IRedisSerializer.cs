using StackExchange.Redis;
using System;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：IRedisSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：redis存储解析类，用于类型与字符串之间的转换
    /// 创建标识：yjq 2017/4/28 16:29:27
    /// </summary>
    public interface IRedisSerializer
    {
        object Deserialize(RedisValue objbyte);

        T Deserialize<T>(RedisValue objbyte);

        string Serialize(object value);

        string Serialize<T>(T value);


        string Serialize(object value, Type type);

        string Serialize<T>(T value, Type type);
    }
}