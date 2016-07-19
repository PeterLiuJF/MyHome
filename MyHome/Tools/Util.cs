using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MyHome.Tools
{
    public static class Util
    {
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
    }
}