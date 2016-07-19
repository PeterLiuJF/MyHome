using MyHome.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyHome.Models
{
    public class UserInfo
    {
        public int ID { get; set; }

        [DisplayName("用户名")]
        [Required, StringLength(15)]
        public string UserName { get; set; }

        [DisplayName("密码")]
        [Required, StringLength(15, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DisplayName("角色")]
        public Role Role { get; set; }
        [Required, DisplayName("性别")]
        public Sex Sex { get; set; }

        [DisplayName("身份证"), StringLength(18)]
        [RegularExpression(@"\d{17}[\d|x]|\d{15}", ErrorMessage = "身份证号码格式错误")]
        public string IDCard { get; set; }
        [DisplayName("QQ"), StringLength(30)]
        public string QQ { get; set; }
        [DisplayName("邮箱"), EmailAddress, StringLength(30)]
        public string Email { get; set; }
        [DisplayName("手机号"), StringLength(20)]
        public string Phone { get; set; }
        [DisplayName("出生日期")]
        public DateTime? BirthDay { get; set; }
        [DisplayName("现居住地址")]
        public virtual AddressInfo Address { get; set; }
        [DisplayName("婚姻")]
        public Marriage Marriage { get; set; }
    }
}
