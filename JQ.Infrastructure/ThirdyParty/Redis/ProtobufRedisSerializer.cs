using ProtoBuf;
using StackExchange.Redis;
using System;
using System.IO;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：ProtobufRedisSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：Protobuf序列化与反序列化
    /// 创建标识：yjq 2017/4/28 16:47:47
    /// </summary>
    public sealed class ProtobufRedisSerializer : DefaultRedisSerializer, IRedisSerializer
    {
        private const string TYPESEPERATOR = "|";
        private const string PROTOBUFPREFIX = "PB^";

        public override object Deserialize(RedisValue objbyte)
        {
            return Deserialize<object>(objbyte);
        }

        public override T Deserialize<T>(RedisValue objbyte)
        {
            string serializedObj = objbyte;
            if (!serializedObj.StartsWith(PROTOBUFPREFIX))
            {
                return base.Deserialize<T>(objbyte);
            }

            serializedObj = serializedObj.Substring(PROTOBUFPREFIX.Length);
            var typeSeperatorIndex = serializedObj.IndexOf(TYPESEPERATOR, StringComparison.InvariantCultureIgnoreCase);
            var serialized = serializedObj.Substring(typeSeperatorIndex + 1);
            var byteAfter64 = Convert.FromBase64String(serialized);

            using (var memoryStream = new MemoryStream(byteAfter64))
            {
                return Serializer.Deserialize<T>(memoryStream);
            }
        }

        public override string Serialize(object value, Type type)
        {
            return Serialize<object>(value, type);
        }

        public override string Serialize<T>(T value, Type type)
        {
            if (!type.IsDefined(typeof(ProtoContractAttribute), false))
            {
                return base.Serialize(value, type);
            }

            using (var memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, value);
                var serialized = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
                return $"{PROTOBUFPREFIX}{type.AssemblyQualifiedName}{TYPESEPERATOR}{serialized}";
            }
        }

        public override string Serialize(object value)
        {
            return Serialize<object>(value);
        }

        public override string Serialize<T>(T value)
        {
            return Serialize(value, value.GetType());
        }
    }
}