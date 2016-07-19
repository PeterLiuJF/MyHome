using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHome.Models
{
    public class AddressInfo
    {
        [ForeignKey("UserInfo"), Key]
        public int UserInfoID { get; set; }
        public UserInfo UserInfo { get; set; }
        
        [DisplayName("省"), StringLength(20)]
        public string Province { get; set; }
        [DisplayName("市"), StringLength(20)]
        public string City { get; set; }
        [DisplayName("县"), StringLength(20)]
        public string County { get; set; }
        [DisplayName("地址"), StringLength(50)]
        public string Address { get; set; }
    }
}