using ProtoBuf;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

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
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, bool> _isHaveProtoContractCache = new ConcurrentDictionary<RuntimeTypeHandle, bool>();

        public override object Deserialize(RedisValue objbyte)
        {
            return Deserialize<object>(objbyte);
        }

        public override Task<object> DeserializeAsync(RedisValue objbyte)
        {
            return DeserializeAsync<object>(objbyte);
        }

        public override T Deserialize<T>(RedisValue objbyte)
        {
            var type = typeof(T);
            if (!IsHaveProtoContract(type))
            {
                return base.Deserialize<T>(objbyte);
            }
            using (var memoryStream = new MemoryStream(objbyte))
            {
                return Serializer.Deserialize<T>(memoryStream);
            }

        }

        public override Task<T> DeserializeAsync<T>(RedisValue objbyte)
        {
            return Task.Factory.StartNew(() => Deserialize<T>(objbyte));
        }

        public override byte[] Serialize(object value)
        {
            return Serialize<object>(value);
        }

        public override Task<byte[]> SerializeAsync(object value)
        {
            return SerializeAsync(value);
        }

        public override byte[] Serialize<T>(T value)
        {
            var type = value.GetType();
            if (!IsHaveProtoContract(type))
            {
                return base.Serialize(value);
            }

            using (var memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, value);
                return memoryStream.ToArray();
            }
        }

        public override Task<byte[]> SerializeAsync<T>(T value)
        {
            return Task.Factory.StartNew(() => Serialize(value));
        }

        private bool IsHaveProtoContract(Type type)
        {
            RuntimeTypeHandle typeHandle = type.TypeHandle;
            if (!_isHaveProtoContractCache.ContainsKey(typeHandle))
            {
                _isHaveProtoContractCache[typeHandle] = type.IsDefined(typeof(ProtoContractAttribute), false);
            }
            return _isHaveProtoContractCache[typeHandle];
        }
    }
}