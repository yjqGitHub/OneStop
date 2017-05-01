using Autofac;
using JQ.Infrastructure;
using System;

namespace JQ.InfrastructureUnitTest
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：BaseTest.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：基础测试类
    /// 创建标识：yjq 2017/5/1 16:30:45
    /// </summary>
    public class BaseTest : IDisposable
    {
        public BaseTest()
        {
            ObjectContainer.SetContainer(new AutofacObjectContainer(new ContainerBuilder()));
            ObjectContainer.Register<ILoggerFactory, NLogFactory>();
            ObjectContainer.Register<IJsonSerializer, NewtonsoftJsonSerializer>();
        }

        public void Dispose()
        {
            ConnectionMultiplexerFactory.DisposeConn();
        }
    }
}