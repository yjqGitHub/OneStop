using JQ.Infrastructure;
using Xunit;

namespace JQ.InfrastructureUnitTest.ThirdyParty
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：AutofacObjectContainerTest.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：AutofacObjectContainerTest
    /// 创建标识：yjq 2017/4/27 22:32:19
    /// </summary>
    public sealed class AutofacObjectContainerTest : BaseTest
    {
        public AutofacObjectContainerTest() : base()
        {
            ObjectContainer.Register<ISerializer, ProtobufSerializer>(serviceName: "protobufSerializer", life: LifeStyle.RequestLifetimeScope);
            ObjectContainer.Register<IRedisSerializer, DefaultRedisSerializer>(serviceName: "defaultSerializer", life: LifeStyle.RequestLifetimeScope);
            ObjectContainer.Register<IRedisSerializer, ProtobufRedisSerializer>(life: LifeStyle.RequestLifetimeScope);
        }

        [Fact]
        public void Test()
        {
            LogUtil.Debug("11111");
            var serializer = ObjectContainer.ResolveNamed<ISerializer>("protobufSerializer");
            Assert.IsType(typeof(ProtobufSerializer), serializer);
            var redisSerialize = ObjectContainer.Resolve<IRedisSerializer>();
            Assert.IsType(typeof(ProtobufRedisSerializer), redisSerialize);
            var redisSerializeDefault = ObjectContainer.ResolveNamed<IRedisSerializer>("defaultSerializer");
            Assert.IsType(typeof(DefaultRedisSerializer), redisSerializeDefault);
        }
    }
}