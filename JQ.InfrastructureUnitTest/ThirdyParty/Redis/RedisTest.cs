using JQ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JQ.InfrastructureUnitTest.ThirdyParty.Redis
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：RedisTest.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：redis测试类
    /// 创建标识：yjq 2017/5/1 16:33:48
    /// </summary>
    public sealed class RedisTest:BaseTest
    {

        public RedisTest() : base()
        {
            ObjectContainer.Register<IRedisSerializer, DefaultRedisSerializer>(serviceName: "Default", life: LifeStyle.RequestLifetimeScope);
            ObjectContainer.Register<IRedisSerializer, ProtobufRedisSerializer>(serviceName: "Protobuf", life: LifeStyle.RequestLifetimeScope);
            ObjectContainer.Register<IRedisDatabaseProvider, StackExchangeRedisProvider>(life: LifeStyle.Singleton);
        }

        [Fact]
        public void StringSetTest()
        {
            var redisProvider = ObjectContainer.Resolve<IRedisDatabaseProvider>();
            var redisClient =redisProvider.CreateDefaultConfigClient();
            redisClient.StringSet("test", new { Id = 1 });
            dynamic obj =redisClient.StringGet<dynamic>("test");
            Assert.NotNull(obj);
            Assert.Same(1, obj.Id);
        }
    }
}
