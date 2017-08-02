using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace pc.Models
{
    public class ChangeMemView
    {
        [DisplayName("姓名")]
        [Required(ErrorMessage = "請輸入姓名")]
        public string name { get; set; }

        [DisplayName("電話")]
        [Required(ErrorMessage = "請輸入電話")]
        public string phone { get; set; }

        [DisplayName("地址")]
        [Required(ErrorMessage = "請輸入地址")]
        public string address { get; set; }

        [DisplayName("email")]
        [Required(ErrorMessage = "請輸入電子郵件")]
        [RegularExpression(@"[a-zA-Z0-9.%+-]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,4}",ErrorMessage="請輸入電子郵件格式")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
    }
}