using Autofac;
using JQ.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace JQ.InfrastructureUnitTest
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BaseTest.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：BaseTest
    /// 创建标识：yjq 2017/5/1 22:49:25
    /// </summary>
    [Collection("ContainerCollection")]
    public class BaseTest
    {
        private ITestOutputHelper _output;

        public BaseTest(ITestOutputHelper output)
        {
            _output = output;
            ObjectContainer.SetContainer(new AutofacObjectContainer(new ContainerBuilder()));
            ObjectContainer.Register<ILoggerFactory, NLogFactory>();
            ObjectContainer.Register<IJsonSerializer, NewtonsoftJsonSerializer>();
            _output.WriteLine("111");
        }

        public ITestOutputHelper TestOutputHelper { get { return _output; } }
    }
}