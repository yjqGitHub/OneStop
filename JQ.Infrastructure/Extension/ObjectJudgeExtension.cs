﻿namespace JQ.Infrastructure.Extension
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：ObjectJudgeExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：判断扩展类
    /// 创建标识：yjq 2017/4/28 15:53:45
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 判断类型为Null
        /// </summary>
        /// <param name="input">要判断的信息</param>
        /// <returns>Null的时候为true</returns>
        public static bool IsNull(this object input)
        {
            return input == null;
        }

        /// <summary>
        /// 判断类型不为Null
        /// </summary>
        /// <param name="input">要判断的信息</param>
        /// <returns>Null的时候为false</returns>
        public static bool IsNotNull(this object input)
        {
            return !(input.IsNull());
        }

        /// <summary>
        /// 判断字符串为Null或者为空
        /// </summary>
        /// <param name="input">要判断的信息</param>
        /// <returns>如果为Null或者为空则返回true</returns>
        public static bool IsNullOrEmpty(this object input)
        {
            if (input == null) return true;
            return string.IsNullOrEmpty(input.ToString());
        }

        /// <summary>
        /// 判断字符串不为Null并且不为空
        /// </summary>
        /// <param name="input">要判断的信息</param>
        /// <returns>如果不为Null且不为空则返回true</returns>
        public static bool IsNotNullAndEmpty(this object input)
        {
            if (input == null) return false;
            if (string.IsNullOrEmpty(input.ToString())) return false;
            return true;
        }

        /// <summary>
        /// 判断字符串为Null或者为空且或者由空白字符组成
        /// </summary>
        /// <param name="input">要判断的信息</param>
        /// <returns>如果为Null且或者为空且或者由空白字符组成则返回true</returns>
        public static bool IsNullOrEmptyWhiteSpace(this object input)
        {
            if (input == null) return true;
            return string.IsNullOrWhiteSpace(input.ToString());
        }

        /// <summary>
        /// 判断字符串不为Null并且不为空且不是由空白字符组成
        /// </summary>
        /// <param name="input">要判断的信息</param>
        /// <returns>如果不为Null且不为空且不是由空白字符组成则返回true</returns>
        public static bool IsNotNullAndEmptyWhiteSpace(this object input)
        {
            if (input == null) return false;
            if (string.IsNullOrWhiteSpace(input.ToString())) return false;
            return true;
        }
    }
}