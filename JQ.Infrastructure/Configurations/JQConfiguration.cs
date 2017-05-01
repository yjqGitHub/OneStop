namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：JQConfiguration.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：基础配置信息
    /// 创建标识：yjq 2017/4/28 11:00:12
    /// </summary>
    public class JQConfiguration
    {
        /// <summary>
        /// 默认日志记录器名字
        /// </summary>
        public static string DefaultLoggerName { get; set; } = "JQ.DefaultLogger";

        /// <summary>
        /// appconfi的路径
        /// </summary>
        public static string AppConfigPath { get; set; }

        /// <summary>
        /// IP解析的文件路径
        /// </summary>
        public static string IPParseConfigPath { get; set; }

        /// <summary>
        /// redis默认前缀
        /// </summary>
        public static string RedisPrfix { get; set; }
    }
}