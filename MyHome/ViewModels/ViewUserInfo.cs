using MyHome.Enums;
using MyHome.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHome.ViewModels
{
    public class ViewUserInfo
    {
        public int ID { get; set; }

        [DisplayName("用户名")]
        [Required(ErrorMessage = "请输入{0}"), StringLength(15, ErrorMessage = "最多15个字符")]
        [Remote("CheckUserName", "UserInfoes", ErrorMessage = "用户名已存在")]
        public string UserName { get; set; }

        [DisplayName("密码")]
        [Required(ErrorMessage = "请输入{0}"), StringLength(15, MinimumLength = 6, ErrorMessage = "密码长度必须在{2}和{1}位之间")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("确认密码")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "两次密码不一致，请重新输入")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }
        [Required, DisplayName("角色")]
        public Role Role { get; set; }
        [Required, DisplayName("性别")]
        public Sex Sex { get; set; }
    }

    public class UserLoginModel
    {
        [Required(ErrorMessage ="请输入{0}"), Display(Name = "用户名")]
        public string UserName { get; set; }

        [StringLength(15, MinimumLength = 6)]
        [Required(ErrorMessage = "请输入{0}"), Display(Name = "用户密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }
}