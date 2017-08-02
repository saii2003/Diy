using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pc.Models
{
    public class IndexView
    {
        public List<product> products { get; set; }

        public string cpu { get; set; }
        public string cpu_amount { get; set; }
        public string cpu_price { get; set; }

        public string mainboard { set; get; }
        public string mainboard_amount { get; set; }
        public string mainboard_price { get; set; }

        public string memory { get; set; }
        public string memory_amount { get; set; }
        public string memory_price { get; set; }

        public string hardware { set; get; }
        public string hardware_amount { get; set; }
        public string hardware_price { get; set; }

        public string videocard { set; get; }
        public string videocard_amount { get; set; }
        public string videocard_price { get; set; }

        public string power { get; set; }
        public string power_amount { get; set; }
        public string power_price { get; set; }

        public string cases { set; get; }
        public string cases_amount { get; set; }
        public string cases_price { get; set; }

        public string dvd { set; get; }
        public string dvd_amount { get; set; }
        public string dvd_price { get; set; }

        public string monitor { get; set; }
        public string monitor_amount { get; set; }

    }
}