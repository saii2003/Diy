using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace pc.Models
{
    public class orderEditView
    {
        [Required(ErrorMessage = "請輸入收件者")]
        public string receiver { get; set; }

        [Required(ErrorMessage = "請輸入電話")]
        public string phone { get; set; }

        [Required(ErrorMessage = "請輸入地址")]
        public string address { get; set; }

        [Required(ErrorMessage = "請輸入價格")]
        public decimal price { get; set; }

     
        public bool payment { get; set; }
        public bool shipment { get; set; }

    }
}