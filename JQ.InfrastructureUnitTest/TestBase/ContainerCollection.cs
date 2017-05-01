using Xunit;

namespace JQ.InfrastructureUnitTest
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：BaseTest.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：基础测试类
    /// 创建标识：yjq 2017/5/1 16:30:45
    /// </summary>
    [CollectionDefinition("DatabaseCollection")]
    public class ContainerCollection : ICollectionFixture<ContainerFixture>
    {
    }
}