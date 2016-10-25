using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MyHome.Tools
{
    public static class Util
    {
        public static string[] ImageFileTypes = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        public static string ToChineseString(this bool value) => value ? "是" : "否";

        public static string ToChineseString(this bool? value) => ToChineseString(value.Value);
        public static string ToMD5(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string pwd = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(text)), 3, 6);
            pwd = pwd.Replace("-", "");
            return pwd;
        }

        public static List<T> ToEnumList<T>()
        {
            var type = typeof(T);
            var EnumList = new List<T>();
            foreach (var value in Enum.GetValues(type))
            {
                var name = Enum.GetName(type, value);
                EnumList.Add((T)value);
            }
            return EnumList;
        }

        #region 枚举转换成下拉列表

        /// <summary>
        /// 将list转换selectList
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectListItem(List<string> values, string select)
        {
            var list = new List<SelectListItem>();
            foreach (var value in values)
            {
                list.Add(new SelectListItem
                {
                    Text = value,
                    Value = value,
                    Selected = select == value
                });
            }
            return list;
        }

        #endregion

        #region Pare(Int,String,Bool)
        /// <summary>
        /// 字符串转换枚举
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="enumName"></param>
        /// <returns></returns>
        public static T GetEnumByString<T>(string enumName)
        {
            return (T)Enum.Parse(typeof(T), enumName);
        }

        /// <summary>
        /// 将 String 类型转换为 int 型,转换失败返回 0
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <returns>返回 int</returns>
        public static int ParseInt(string strValue)
        {
            int returnValue = 0;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!int.TryParse(strValue, out returnValue))
                {
                    returnValue = 0;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 int 型,转换失败时返回 0
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 int</returns>
        public static int ParseInt(object objValue)
        {
            int returnValue = 0;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!int.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = 0;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 String 型,转换失败时返回 ""
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 String</returns>
        public static string ParseString(object objValue)
        {
            string returnValue = "";

            if (objValue != null && objValue != DBNull.Value)
            {
                returnValue = objValue.ToString();
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 bool 型,转换失败时返回 false
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 bool</returns>
        public static bool ParseBool(object objValue)
        {
            bool returnValue = false;

            if (objValue != null && objValue != DBNull.Value)
            {
                var valueText = objValue.ToString();
                if (valueText == "1")
                    return true;
                else if (valueText == "0")
                    return false;

                if (!bool.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }
        #endregion

        public static string GetThumbFilePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            var extName = Path.GetExtension(path);
            var fileName = string.Format("{0}_small{1}", Path.GetFileNameWithoutExtension(path), extName);
            return Path.Combine(Path.GetDirectoryName(path), fileName);
        }

        //// <summary> 
        /// 生成缩略图 
        /// </summary> 
        /// <param name="originalImagePath">源图路径（物理路径）</param> 
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param> 
        /// <param name="width">缩略图宽度</param> 
        /// <param name="height">缩略图高度</param> 
        /// <param name="mode">生成缩略图的方式</param>     
        public static bool MakeThumbnail(string originalImagePath)
        {
            try
            {
                var extName = Path.GetExtension(originalImagePath).ToLower();
                if (!IsImageFile(extName))
                    return false;

                Image originalImage = Image.FromFile(originalImagePath);
                int width = 210;
                int height = 160;
                int towidth = width;
                int toheight = height;

                int x = 0;
                int y = 0;
                int ow = originalImage.Width;
                int oh = originalImage.Height;
                var mode = "HW";

                switch (mode)
                {
                    case "HW"://指定高宽缩放（可能变形）                 
                        break;
                    case "W"://指定宽，高按比例                     
                        toheight = originalImage.Height * width / originalImage.Width;
                        break;
                    case "H"://指定高，宽按比例 
                        towidth = originalImage.Width * height / originalImage.Height;
                        break;
                    case "Cut"://指定高宽裁减（不变形）                 
                        if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                        {
                            oh = originalImage.Height;
                            ow = originalImage.Height * towidth / toheight;
                            y = 0;
                            x = (originalImage.Width - ow) / 2;
                        }
                        else
                        {
                            ow = originalImage.Width;
                            oh = originalImage.Width * height / towidth;
                            x = 0;
                            y = (originalImage.Height - oh) / 2;
                        }
                        break;
                    default:
                        break;
                }
                //新建一个bmp图片 
                Image bitmap = new Bitmap(towidth, toheight);
                //新建一个画板 
                Graphics g = Graphics.FromImage(bitmap);

                //清空画布并以透明背景色填充 
                g.Clear(Color.Transparent);

                //在指定位置并且按指定大小绘制原图片的指定部分 
                g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                    new Rectangle(x, y, ow, oh),
                    GraphicsUnit.Pixel);

                var thumbnailPath = GetThumbFilePath(originalImagePath);
                //以jpg格式保存缩略图 
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsImageFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return false;
            var extName = Path.GetExtension(fileName).ToLower();
            return ImageFileTypes.Contains(extName);
        }
    }
}