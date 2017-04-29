using System.Threading.Tasks;

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
        object Deserialize(byte[] objbyte);

        Task<object> DeserializeAsync(byte[] objbyte);

        T Deserialize<T>(byte[] objbyte);

        Task<T> DeserializeAsync<T>(byte[] objbyte);

        byte[] Serialize(object value);

        Task<byte[]> SerializeAsync(object value);

        byte[] Serialize<T>(T value);

        Task<byte[]> SerializeAsync<T>(T value);
    }
}