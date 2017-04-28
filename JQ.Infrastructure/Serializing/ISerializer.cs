using System.Threading.Tasks;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ISerializer.cs
    /// 类属性：公共接口（非静态）
    /// 类功能描述：序列化接口
    /// 创建标识：yjq 2017/4/27 20:43:32
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
		byte[] Serialize(object item);

        /// <summary>
        /// 异步序列化
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<byte[]> SerializeAsync(object item);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <returns></returns>
        object Deserialize(byte[] serializedObject);

        /// <summary>
        /// 异步反序列化
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <returns></returns>
        Task<object> DeserializeAsync(byte[] serializedObject);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializedObject"></param>
        /// <returns></returns>
        T Deserialize<T>(byte[] serializedObject);

        /// <summary>
        /// 异步反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializedObject"></param>
        /// <returns></returns>
        Task<T> DeserializeAsync<T>(byte[] serializedObject);
    }
}