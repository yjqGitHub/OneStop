using JQ.Infrastructure;
using ProtoBuf;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace JQ.InfrastructureUnitTest
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ProtobufSerializerTest.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ProtobufSerializerTest
    /// 创建标识：yjq 2017/4/27 20:50:31
    /// </summary>
    public class ProtobufSerializerTest : IDisposable
    {
        [Fact]
        public void TestSerialize()
        {
            ISerializer serializer = new ProtobufSerializer();
            var aa = new TestProtobufUser() { Age = 1 };
            var bytes = serializer.Serialize(aa);
            TestProtobufUser desialize = serializer.Deserialize<TestProtobufUser>(bytes);
            Assert.Equal(1, desialize.Age);
        }

        [Fact]
        public async Task TestSerializeAsync()
        {
            ISerializer serializer = new ProtobufSerializer();
            var aa = new TestProtobufUser() { Age = 1 };
            var bytes =await serializer.SerializeAsync(aa);
            TestProtobufUser desialize = await serializer.DeserializeAsync<TestProtobufUser>(bytes);
            Assert.Equal(1, desialize.Age);
        }

        public void Dispose()
        {
            Trace.WriteLine("ProtobufSerializerTest执行结束");
        }

        [ProtoContract]
        public class TestProtobufUser
        {
            [ProtoMember(1)]
            public string User { get; set; }

            [ProtoMember(2)]
            public int Age { get; set; }
        }
    }
}