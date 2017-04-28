using JQ.Infrastructure.Extension;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：RedisCacheOptions.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：Redis配置信息
    /// 创建标识：yjq 2017/4/28 15:40:52
    /// </summary>
    public sealed class RedisCacheOptions
    {
        private const string CONFIGKEY_CONNECTION = "JQ.Redis.Connection";

        private const string CONFIGKEY_DATABASEID = "JQ.Redis.DatabaseId";

        public string ConnectionString { get; set; }

        public int DatabaseId { get; set; }

        public RedisCacheOptions()
        {
            ConnectionString = GetDefaultConnectionString();
            DatabaseId = GetDefaultDatabaseId();
        }

        private static string GetDefaultConnectionString()
        {
            string configSet = ConfigUtil.GetValue(CONFIGKEY_CONNECTION, memberName: "RedisCacheOptions-GetDefaultConnectionString");
            return configSet.IsNullOrEmptyWhiteSpace() ? "localhost" : configSet;
        }

        private static int GetDefaultDatabaseId()
        {
            string configSet = ConfigUtil.GetValue(CONFIGKEY_DATABASEID, memberName: "RedisCacheOptions-GetDefaultDatabaseId");
            if (configSet.IsNullOrEmptyWhiteSpace())
            {
                return -1;
            }
            return configSet.ToSafeInt32(-1);
        }
    }
}