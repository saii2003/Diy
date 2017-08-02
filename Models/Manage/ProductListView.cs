using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using pc.Models;
using pc.Service;

namespace pc.Models
{
    public class ProductListView
    {
        public List<product> dataList { get; set; }

        public string cid { get; set; }
        public string search { get; set; }
        public PageService page { set; get; }
    }
}