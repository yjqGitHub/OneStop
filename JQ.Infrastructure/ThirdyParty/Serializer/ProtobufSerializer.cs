using ProtoBuf;
using System.IO;
using System.Threading.Tasks;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ProtobufSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：自定义异常类
    /// 创建标识：yjq 2017/4/27 20:09:58
    /// </summary>
    public sealed class ProtobufSerializer : ISerializer
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public byte[] Serialize(object item)
        {
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, item);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 异步序列化
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<byte[]> SerializeAsync(object item)
        {
            return Task.Factory.StartNew(() => Serialize(item));
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <returns></returns>
        public object Deserialize(byte[] serializedObject)
        {
            return Deserialize<object>(serializedObject);
        }

        /// <summary>
        /// 异步反序列化
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <returns></returns>
        public Task<object> DeserializeAsync(byte[] serializedObject)
        {
            return DeserializeAsync<object>(serializedObject);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializeObject"></param>
        /// <returns></returns>
        public T Deserialize<T>(byte[] serializeObject)
        {
            using (var ms = new MemoryStream(serializeObject))
            {
                return Serializer.Deserialize<T>(ms);
            }
        }

        /// <summary>
        /// 异步反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializeObject"></param>
        /// <returns></returns>
        public Task<T> DeserializeAsync<T>(byte[] serializeObject)
        {
            return Task.Factory.StartNew(() => Deserialize<T>(serializeObject));
        }
    }
}