using System;
using System.Runtime.Serialization;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：JQException.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：自定义异常类
    /// 创建标识：yjq 2017/4/27 17:32:32
    /// </summary>
    [Serializable]
    public sealed class JQException : Exception
    {
        public JQException()
        {
        }

        public JQException(string msg)
            : base(msg)
        {
        }

        public JQException(string msg, Exception innerException)
            : base(msg, innerException)
        {
        }

        public JQException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}