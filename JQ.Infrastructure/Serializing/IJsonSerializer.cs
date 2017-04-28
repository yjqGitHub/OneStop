namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IJsonSerializer.cs
    /// 类属性：公共接口（非静态）
    /// 类功能描述：Json序列化接口
    /// 创建标识：yjq 2017/4/27 21:57:27
    /// </summary>
    public interface IJsonSerializer
    {
        /// <summary>
        /// 序列化成json格式字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        string Serialize<T>(T obj);

        /// <summary>
        /// 根据对象获取json格式的字符串(忽略null值)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">对象值</param>
        /// <returns>json格式的字符串</returns>
        string SerializeIgnoreNull<T>(T obj);

        /// <summary>
        /// 将json格式字符串反序列化成指定类型
        /// </summary>
        /// <typeparam name="T">序列化后类型</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        T Deserialize<T>(string value);
    }
}