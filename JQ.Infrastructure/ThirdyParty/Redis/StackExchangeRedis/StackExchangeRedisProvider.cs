namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：ConnectionMulitiplexerFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ConnectionMultiplexer工厂
    /// 创建标识：yjq 2017/4/28 13:56:26
    /// </summary>
    public sealed class StackExchangeRedisProvider : JQDisposable, IRedisDatabaseProvider
    {
        /// <summary>
        /// 创建默认的redis客户端
        /// </summary>
        /// <returns></returns>
        public IRedisClient CreateDefaultConfigClient()
        {
            RedisCacheOptions redisCacheOption = new RedisCacheOptions();
            IRedisSerializer serializer = ObjectContainer.Resolve<IRedisSerializer>();
            return new StackExchangeRedisClient(redisCacheOption, serializer);
        }

        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <param name="redisCacheOption">redis配置信息</param>
        /// <returns></returns>
        public IRedisClient CreateClient(RedisCacheOptions redisCacheOption)
        {
            IRedisSerializer serializer = ObjectContainer.Resolve<IRedisSerializer>();
            return new StackExchangeRedisClient(redisCacheOption, serializer);
        }

        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <param name="serializer">序列化类</param>
        /// <returns></returns>
        public IRedisClient CreateClient(IRedisSerializer serializer)
        {
            return new StackExchangeRedisClient(new RedisCacheOptions(), serializer);
        }

        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <param name="redisCacheOption">redis配置信息</param>
        /// <param name="serializer">序列化类</param>
        /// <returns></returns>
        public IRedisClient CreateClient(RedisCacheOptions redisCacheOption, IRedisSerializer serializer)
        {
            return new StackExchangeRedisClient(redisCacheOption, serializer);
        }
    }
}