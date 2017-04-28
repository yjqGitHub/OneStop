using System.IO;

namespace JQ.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：FileUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：文件帮助类
    /// 创建标识：yjq 2017/4/28 15:59:45
    /// </summary>
    public static class FileUtil
    {
        /// <summary>
        /// 判断文件是否存在本地目录
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsExistsFile(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 文件是否不存在本地目录
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsNotExistsFile(string filePath)
        {
            return !IsExistsFile(filePath);
        }
    }
}