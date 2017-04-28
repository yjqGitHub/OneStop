using System;

namespace JQ.Infrastructure.Extension
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：ObjectConvertExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：类型转换扩展方法
    /// 创建标识：yjq 2017/4/28 15:52:35
    /// </summary>
    public static partial class Extension
    {
        public static int ToSafeInt32(this object o, int defaultValue)
        {
            if ((o != null) && !string.IsNullOrWhiteSpace(o.ToString()))
            {
                int num;
                string s = o.ToString().Trim().ToLower();
                switch (s)
                {
                    case "true":
                        return 1;

                    case "false":
                        return 0;
                }
                if (int.TryParse(s, out num))
                {
                    return num;
                }
            }
            return defaultValue;
        }

        public static int? ToSafeInt32(this object o)
        {
            if ((o != null) && !string.IsNullOrWhiteSpace(o.ToString()))
            {
                int num;
                string s = o.ToString().Trim().ToLower();
                switch (s)
                {
                    case "true":
                        return 1;

                    case "false":
                        return 0;
                }
                if (int.TryParse(s, out num))
                {
                    return num;
                }
            }
            return null;
        }

        public static long ToSafeInt64(this object o, int defaultValue)
        {
            if ((o != null) && !string.IsNullOrWhiteSpace(o.ToString()))
            {
                long num;
                string s = o.ToString().Trim().ToLower();
                switch (s)
                {
                    case "true":
                        return 1;

                    case "false":
                        return 0;
                }
                if (long.TryParse(s, out num))
                {
                    return num;
                }
            }
            return defaultValue;
        }

        public static long? ToSafeInt64(this object o)
        {
            if ((o != null) && !string.IsNullOrWhiteSpace(o.ToString()))
            {
                long num;
                string s = o.ToString().Trim().ToLower();
                switch (s)
                {
                    case "true":
                        return 1;

                    case "false":
                        return 0;
                }
                if (long.TryParse(s, out num))
                {
                    return num;
                }
            }
            return null;
        }

        public static float ToSafeFloat(this object o, float defValue)
        {
            if (o == null || string.IsNullOrWhiteSpace(o.ToString()))
            {
                return defValue;
            }
            float result = defValue;
            if ((o != null))
            {
                float.TryParse(o.ToString().Trim(), out result);
            }
            return result;
        }

        public static float? ToSafeFloat(this object o)
        {
            if (o != null && !string.IsNullOrWhiteSpace(o.ToString()))
            {
                float result;
                float.TryParse(o.ToString().Trim(), out result);
                return result;
            }
            return null;
        }

        public static decimal ToSafeDecimal(this object o, decimal defValue)
        {
            if (o == null || string.IsNullOrWhiteSpace(o.ToString()))
            {
                return defValue;
            }
            decimal result = defValue;
            if ((o != null))
            {
                decimal.TryParse(o.ToString().Trim(), out result);
            }
            return result;
        }

        public static decimal? ToSafeDecimal(this object o)
        {
            if (o != null && !string.IsNullOrWhiteSpace(o.ToString()))
            {
                decimal result;
                decimal.TryParse(o.ToString().Trim(), out result);
                return result;
            }
            return null;
        }

        public static bool ToSafeBool(this object o, bool defValue)
        {
            if (o != null)
            {
                if (string.Compare(o.ToString().Trim(), "1") == 0)
                {
                    return true;
                }
                if (string.Compare(o.ToString().Trim(), "0") == 0)
                {
                    return false;
                }
                if (string.Compare(o.ToString().Trim(), "true", true) == 0)
                {
                    return true;
                }
                if (string.Compare(o.ToString().Trim(), "false", true) == 0)
                {
                    return false;
                }
            }
            return defValue;
        }

        public static bool? ToSafeBool(this object o)
        {
            if (o != null)
            {
                if (string.Compare(o.ToString().Trim(), "1") == 0)
                {
                    return true;
                }
                if (string.Compare(o.ToString().Trim(), "0") == 0)
                {
                    return false;
                }
                if (string.Compare(o.ToString().Trim(), "true", true) == 0 || o.ToString().Trim() == "1")
                {
                    return true;
                }
                if (string.Compare(o.ToString().Trim(), "false", true) == 0 || o.ToString().Trim() == "0")
                {
                    return false;
                }
            }
            return null;
        }

        public static string ToSafeString(this object obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString();
        }

        /// <summary>
        /// object 转换为自定义格式的字符串
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <param name="format">自定义格式</param>
        /// <param name="defaultValue">object为null时的默认输出</param>
        /// <returns></returns>
        public static string ToFormatedString(this object obj, string format, string defaultValue = "")
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.ToString()))
            {
                return defaultValue;
            }

            if (string.IsNullOrWhiteSpace(format))
            {
                return obj.ToString();
            }

            var formattableObj = obj as IFormattable;
            if (formattableObj == null)
            {
                return obj.ToString();
            }
            else
            {
                return formattableObj.ToString(format, null);
            }
        }
    }
}