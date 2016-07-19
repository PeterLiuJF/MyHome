using MyHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHome.Tools
{
    public static class Cacher
    {
        private static IEnumerable<UserInfo> _userInfoes;
        public static IEnumerable<UserInfo> UserInfoes
        {
            get
            {
                if (_userInfoes == null)
                {
                    var db = new HomeContext();
                    _userInfoes = db.UserInfo.ToArray();
                }
                return _userInfoes;
            }

            set
            {
                _userInfoes = value;
            }
        }
    }
}