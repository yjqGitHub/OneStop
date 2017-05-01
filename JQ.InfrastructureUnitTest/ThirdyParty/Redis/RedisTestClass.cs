using ProtoBuf;

namespace JQ.InfrastructureUnitTest.ThirdyParty.Redis
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RedisTestClass.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：RedisTestClass
    /// 创建标识：yjq 2017/5/1 23:19:08
    /// </summary>
    [ProtoContract]
    public sealed class RedisTestClass
    {
        [ProtoMember(1)]
        public int Age { get; set; } = 1;
        [ProtoMember(2)]
        public int Sex { get; set; } = 0;
        [ProtoMember(3)]
        public string MyProperty1 { get; set; } = "123456789";
        [ProtoMember(4)]
        public string MyProperty2 { get; set; } = "123456789";
        [ProtoMember(5)]
        public string MyProperty3 { get; set; } = "123456789";
        [ProtoMember(6)]
        public string MyProperty4{ get; set; } = "123456789";
        [ProtoMember(7)]
        public string MyProperty5 { get; set; } = "123456789";
        [ProtoMember(8)]
        public string MyProperty6 { get; set; } = "123456789";
        [ProtoMember(9)]
        public string MyProperty7 { get; set; } = "123456789";
    }
}