using MyHome.Models;
using MyHome.Tools;
using MyHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHome.Controllers
{
    public class BaseController : Controller
    {
        protected HomeContext db = new HomeContext();
        public string UserName => User.Identity.Name;
        private ViewUserInfo _userInfo = null;
        public ViewUserInfo CurrentUser
        {
            get
            {
                if (_userInfo == null)
                {
                    var user = Cacher.UserInfoes.SingleOrDefault(t => t.UserName == UserName);
                    _userInfo = user == null ? null : new ViewUserInfo()
                    {
                        ID = user.ID,
                        UserName = user.UserName,
                        Role = user.Role
                    };
                }

                return _userInfo;
            }
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var authRoleatt = filterContext.ActionDescriptor.GetCustomAttributes(false).SingleOrDefault(t => t is AuthorizeRoleAttribute) as AuthorizeRoleAttribute;
            if (authRoleatt == null || CurrentUser == null)
                return;

            if (!authRoleatt.Roles.Contains(CurrentUser.Role))
            {
                filterContext.Result = View("NoPermission", "_Layout", "您没有权限访问此功能!");
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}