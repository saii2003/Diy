using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace pc.Models
{
    public class ChangePwView
    {
        [DisplayName("舊密碼")]
        [Required(ErrorMessage = "請輸入舊密碼")]
        public string password { get; set; }

        [DisplayName("新密碼")]
        [Required(ErrorMessage = "請輸入新密碼")]
        public string newpassword { get; set; }

        [DisplayName("確定新密碼")]
        [Required(ErrorMessage = "請輸入確定新密碼")]
        [Compare("newpassword",ErrorMessage="新密碼和確定新密碼必須一致")]
        public string confirm_password { get; set; }
    }
}