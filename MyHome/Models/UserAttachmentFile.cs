using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHome.Models
{
    public class UserAttachmentFile
    {
        [DisplayName("附件文件ID")]
        [ForeignKey("File"), Key]
        public int FileID { get; set; }
        public virtual AttachmentFile File { get; set; }

        [ForeignKey("UserInfo")]
        [DisplayName("用户"), Required]
        public int UserInfoID { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}