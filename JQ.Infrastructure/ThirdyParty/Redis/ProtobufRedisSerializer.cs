using ProtoBuf;
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
        public override object Deserialize(byte[] objbyte)
        {
            return Deserialize<object>(objbyte);
        }

        public override Task<object> DeserializeAsync(byte[] objbyte)
        {
            return DeserializeAsync<object>(objbyte);
        }

        public override T Deserialize<T>(byte[] objbyte)
        {
            if (!default(T).GetType().IsDefined(typeof(ProtoContractAttribute), false))
            {
                return base.Deserialize<T>(objbyte);
            }
            using (var memoryStream = new MemoryStream(objbyte))
            {
                return Serializer.Deserialize<T>(memoryStream);
            }
        }

        public override Task<T> DeserializeAsync<T>(byte[] objbyte)
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
            if (!value.GetType().IsDefined(typeof(ProtoContractAttribute), false))
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
    }
}