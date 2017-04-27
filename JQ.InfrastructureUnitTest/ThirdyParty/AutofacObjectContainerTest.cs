using JQ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public sealed class AutofacObjectContainerTest
    {
        [Fact]
        public void TestRegister()
        {
            ObjectContainer.SetContainer(new AutofacObjectContainer());
            ObjectContainer.Register<IJsonSerializer, NewtonsoftJsonSerializer>(serviceName: "jsonnet");
            Assert.IsType<IJsonSerializer>(ObjectContainer.ResolveNamed<IJsonSerializer>("jsonnet"));
            var json = ObjectContainer.Resolve<IJsonSerializer>().Serialize(new { Id = 1 });
            Assert.Equal("{\"Id\":1}", json);
        }
    }
}
