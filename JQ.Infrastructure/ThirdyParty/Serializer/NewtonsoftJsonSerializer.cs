using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：NewtonsoftJsonSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：NewtonsoftJsonSerializer
    /// 创建标识：yjq 2017/4/27 21:47:14
    /// </summary>
    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        public static JsonSerializerSettings Settings { get; private set; }

        public static JsonSerializerSettings IgnoreNullSettings { get; private set; }

        static NewtonsoftJsonSerializer()
        {
            Settings = new JsonSerializerSettings
            {
                ContractResolver = new CustomContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
            IgnoreNullSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CustomContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
        }

        #region 根据json格式字符串获取对象

        /// <summary>
        /// 根据json格式字符串获取对象
        /// </summary>
        /// <typeparam name="T">需要获取的对象</typeparam>
        /// <param name="szJson">json格式的字符串</param>
        /// <returns></returns>
        public T Deserialize<T>(string szJson)
        {
            return JsonConvert.DeserializeObject<T>(szJson, Settings);
        }

        #endregion 根据json格式字符串获取对象

        #region 根据对象获取json格式的字符串

        /// <summary>
        /// 根据对象获取json格式的字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">对象值</param>
        /// <returns>json格式的字符串</returns>
        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, Settings);
        }

        /// <summary>
        /// 根据对象获取json格式的字符串(忽略null值)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">对象值</param>
        /// <returns>json格式的字符串</returns>
        public string SerializeIgnoreNull<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, IgnoreNullSettings);
        }

        #endregion 根据对象获取json格式的字符串

        private class CustomContractResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var jsonProperty = base.CreateProperty(member, memberSerialization);
                if (jsonProperty.Writable) return jsonProperty;
                var property = member as PropertyInfo;
                if (property == null) return jsonProperty;
                var hasPrivateSetter = property.GetSetMethod(true) != null;
                jsonProperty.Writable = hasPrivateSetter;
                return jsonProperty;
            }
        }
    }
}