using System;
using System.Text;

namespace JQ.Infrastructure.Extension
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：ExceptionExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：异常扩展方法
    /// 创建标识：yjq 2017/4/28 11:09:02
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 获取错误异常信息
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="memberName">出现异常的方法名字</param>
        /// <returns>错误异常信息</returns>
        public static string ToErrMsg(this Exception ex, string memberName = null)
        {
            StringBuilder errorBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(memberName))
            {
                errorBuilder.AppendFormat("CallerMemberName：{0}", memberName).AppendLine();
            }
            errorBuilder.AppendFormat("Message：{0}", ex.Message).AppendLine();
            if (ex.InnerException != null)
            {
                if (!string.Equals(ex.Message, ex.InnerException.Message, StringComparison.OrdinalIgnoreCase))
                {
                    errorBuilder.AppendFormat("InnerException：{0}", ex.InnerException.Message).AppendLine();
                }
            }
            errorBuilder.AppendFormat("Source：{0}", ex.Source).AppendLine();
            errorBuilder.AppendFormat("StackTrace：{0}", ex.StackTrace).AppendLine();
            if (WebUtil.IsHaveHttpContext())
            {
                errorBuilder.AppendFormat("RealIP：{0}", WebUtil.GetRealIP()).AppendLine();
                errorBuilder.AppendFormat("HttpRequestUrl：{0}", WebUtil.GetHttpRequestUrl()).AppendLine();
                errorBuilder.AppendFormat("UserAgent：{0}", WebUtil.GetUserAgent()).AppendLine();
            }
            return errorBuilder.ToString();
        }
    }
}