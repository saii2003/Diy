﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pc.Models
{
    public class myOrderView
    {
        public IEnumerable<order> order { get; set; }
        public IEnumerable<orderDetail> orderDetail { get; set; }
  
    }
}