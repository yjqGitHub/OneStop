using Autofac;
using JQ.Infrastructure;
using System;

namespace JQ.InfrastructureUnitTest
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ContainerFixture.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ContainerFixture
    /// 创建标识：yjq 2017/5/1 22:36:34
    /// </summary>
    public class ContainerFixture : IDisposable
    {
        public ContainerFixture()
        {
           
        }

        public void Dispose()
        {
            ConnectionMultiplexerFactory.DisposeConn();
        }
    }
}