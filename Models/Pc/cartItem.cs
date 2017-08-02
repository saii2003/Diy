using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pc.Models
{
    public class cartItem
    {
        public string name { get; set; }
        public int amount { get; set; }
        public int price { get; set; }

        public int one_total
        {
            get { return this.price * this.amount; }
        }
    }
}