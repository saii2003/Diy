using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace pc.Models.Manage
{
    public class ordersView
    {
        public IEnumerable<order> order { get; set; }
        public IEnumerable<orderDetail> orderDetail { get; set; }

   
        public string searchStr { get; set; }
        public string category { get; set; }
    }
}