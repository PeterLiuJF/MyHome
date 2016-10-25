using MyHome.Enums;
using MyHome.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHome.Models
{
    public class AttachmentFile
    {
        public int ID { get; set; }
        public Attachment Attachment { get; set; }
        [DisplayName("文件子路径")]
        public string Path { get; set; }

        [DisplayName("文件绝对路径")]
        [NotMapped]
        public string AbsPath
        {
            get
            {
                return System.IO.Path.Combine(DataDictionary.AppPath, Path.TrimStart('\\'));
            }
        }

        [DisplayName("文件缩略图子路径")]
        [NotMapped]
        public string ThumbPath
        {
            get
            {
                if (Path.EndsWith(".pdf"))
                    return @"\Content\Images\pdf.png";
                if (Path.EndsWith(".xls") || Path.EndsWith(".xlsx"))
                    return @"\Content\Images\Excel.png";
                if (Path.EndsWith(".avi") || Path.EndsWith(".mp4") || Path.EndsWith(".rmvb") || Path.EndsWith(".mkv") || Path.EndsWith(".wmv"))
                    return @"\Content\Images\video.jpg";
                if (Path.EndsWith(".mp3") || Path.EndsWith(".amr") || Path.EndsWith(".wma") || Path.EndsWith(".wav"))
                    return @"\Content\Images\recordSound.jpg";
                return Util.GetThumbFilePath(Path);
            }
        }
        [NotMapped]
        public bool IsImage => Util.IsImageFile(Path);
    }
}