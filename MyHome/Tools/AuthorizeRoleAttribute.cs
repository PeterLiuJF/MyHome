using MyHome.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHome.Tools
{
    public class AuthorizeRoleAttribute:Attribute
    {
        public List<Role> Roles { get; set; }
        public AuthorizeRoleAttribute(params Role[] roles) {
            Roles = new List<Role>(roles);
        }
    }
}