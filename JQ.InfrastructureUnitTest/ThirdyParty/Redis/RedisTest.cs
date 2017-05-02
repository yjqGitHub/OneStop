using JQ.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace JQ.InfrastructureUnitTest.ThirdyParty.Redis
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：RedisTest.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：redis测试类
    /// 创建标识：yjq 2017/5/1 16:33:48
    /// </summary>
    [Collection("ContainerCollection")]
    public sealed class RedisTest : BaseTest
    {
        public RedisTest(ITestOutputHelper output) : base(output)
        {
            ObjectContainer.Register<IRedisSerializer, DefaultRedisSerializer>(serviceName: "Default", life: LifeStyle.RequestLifetimeScope);
            ObjectContainer.Register<IRedisSerializer, ProtobufRedisSerializer>(serviceName: "Protobuf", life: LifeStyle.RequestLifetimeScope);
            ObjectContainer.Register<IRedisDatabaseProvider, StackExchangeRedisProvider>(life: LifeStyle.Singleton);
        }

        [Fact]
        public void StringSetTest()
        {
            TestOutputHelper.WriteLine("222");
            var redisProvider = ObjectContainer.Resolve<IRedisDatabaseProvider>();
            var redisClient = redisProvider.CreateDefaultConfigClient();
            redisClient.StringSet("test2", 1);
            Assert.True(redisClient.StringGet<int>("test2") == 1);
            redisClient.StringSet("test1", new { Id = 1, Name = "12344", Value = "7758258" });
            redisClient.StringSet("test", new { Id = 1 });
            dynamic obj = redisClient.StringGet<dynamic>("test");
            Assert.NotNull(obj);
            Assert.True(1 == obj.Id.Value);

            RedisTestClass test = new Redis.RedisTestClass() { Age = 2 };
            redisClient.StringSet("TestClass", test);
            var testClass = redisClient.StringGet<RedisTestClass>("TestClass");
            Assert.True(testClass.Age == 2);
        }
    }
}