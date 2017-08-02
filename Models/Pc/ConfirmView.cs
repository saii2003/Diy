using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace pc.Models
{
    public class ConfirmView
    {
        [DisplayName("收件者")]
        [Required(ErrorMessage="請輸入收件者")]
        public string receiver { get; set; }

        [DisplayName("收件者電話")]
        [Required(ErrorMessage = "請輸入收件者電話")]
        public string phone { get; set; }

        [DisplayName("收件地址")]
        [Required(ErrorMessage = "請輸入收件地址")]
        public string address { get; set; }

        

    }
}