using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace pc.Models
{
    public class MemberLoginView
    {
        [DisplayName("使用者名稱")]
        [Required(ErrorMessage="請輸入使用者名稱")]
        public string username { get; set; }
        
        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        public string password { get; set; }

        [DisplayName("驗證碼")]
        [Required(ErrorMessage = "請輸入驗證碼")]
        public string validatecode { get; set; }

        [DisplayName("記住我")]
        public bool rememberMe { get; set; }
    }
}