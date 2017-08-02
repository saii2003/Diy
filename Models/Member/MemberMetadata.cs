using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace pc.Models
{
    [MetadataType(typeof(memberMetadata))]
    public partial class member
    {
        private class memberMetadata
        {
            [DisplayName("使用者名稱")]
            [Required(ErrorMessage = "請輸入使用者名稱")]
            [Remote("checkusername", "Member",ErrorMessage="使用者名稱已經使用")]
            public string username { get; set; }

            [DisplayName("姓名")]
            [Required(ErrorMessage = "請輸入姓名")]
            public string name { get; set; }

            [DisplayName("性別")]
            [Required(ErrorMessage = "請選擇性別")]
            public string gender { get; set; }

            [DisplayName("生日")]
            [Required(ErrorMessage = "請選擇日期")]
            [DataType(DataType.Date,ErrorMessage="請輸入日期格式")]
            public string birthday { get; set; }

            [DisplayName("電話")]
            [Required(ErrorMessage = "請輸入電話")]
            public string phone { get; set; }

            [DisplayName("地址")]
            [Required(ErrorMessage = "請輸入地址")]
            public string address { get; set; }

            [DisplayName("電子郵件")]
            [Required(ErrorMessage = "請輸入電子郵件")]
            [DataType(DataType.EmailAddress, ErrorMessage = "請輸入電子郵件格式")]
            public string email { get; set; }

            public string authcode { get; set; }
        }
    }
}