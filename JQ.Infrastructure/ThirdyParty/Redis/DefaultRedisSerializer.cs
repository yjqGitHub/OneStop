using JQ.Infrastructure.Extension;
using System.Text;
using System.Threading.Tasks;

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
        public virtual object Deserialize(byte[] objbyte)
        {
            return Deserialize<object>(objbyte);
        }

        public virtual T Deserialize<T>(byte[] objbyte)
        {
            var jsonString = Encoding.UTF8.GetString(objbyte);
            return jsonString.ToObjInfo<T>();
        }

        public virtual Task<object> DeserializeAsync(byte[] objbyte)
        {
            return DeserializeAsync<object>(objbyte);
        }

        public virtual Task<T> DeserializeAsync<T>(byte[] objbyte)
        {
            return Task.Factory.StartNew(() => Deserialize<T>(objbyte));
        }

        public virtual byte[] Serialize(object value)
        {
            return Serialize<object>(value);
        }

        public virtual byte[] Serialize<T>(T value)
        {
            var jsonString = value.ToJson();
            return Encoding.UTF8.GetBytes(jsonString);
        }

        public virtual Task<byte[]> SerializeAsync(object value)
        {
            return SerializeAsync(value);
        }

        public virtual Task<byte[]> SerializeAsync<T>(T value)
        {
            return Task.Factory.StartNew(() => Serialize(value));
        }
    }
}