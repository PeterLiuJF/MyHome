using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MyHome.Tools
{
    public static class DataDictionary
    {
        /// <summary>
        /// 上传文件夹名
        /// </summary>
        public static readonly string UploadFolderPath = @"Uploads";
        /// <summary>
        /// 应用程序绝对路径
        /// </summary>
        public static readonly string AppPath = HttpRuntime.AppDomainAppPath;

        public static readonly string TemplatesPath = Path.Combine(AppPath, @"Templates\");
    }
}