using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace pc.Models
{
    public class MemberRegView
    {
        public member members { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage="請輸入密碼")]
        public string password { get; set; }

        [DisplayName("確定密碼")]
        [Required(ErrorMessage = "請輸入確認密碼")]
        [Compare("password",ErrorMessage="密碼和確認密碼必須一致")]
        public string confirm_password { get; set; }


        
    }
}