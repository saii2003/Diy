using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace pc.Models
{
    public class SetPwView
    {
        [DisplayName("新密碼")]
        [Required(ErrorMessage = "請輸入新密碼")]
        public string new_password { get; set; }

        [DisplayName("確定新密碼")]
        [Required(ErrorMessage = "請輸入確定新密碼")]
        [Compare("new_password", ErrorMessage = "新密碼和確定新密碼需一致")]
        public string confirm_password { get; set; }


    }
}