using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace pc.Models
{
    public class UpdateProductView
    {
        public string previousUrl { get; set; }

        [Required(ErrorMessage="請輸入商品名稱")]
        public string name { get; set; }

        [Required(ErrorMessage="請選擇商品分類")]
        public int cid { get; set; }

        [Required(ErrorMessage = "請輸入售價")]
        public int price { get; set; }

        [Required(ErrorMessage="請輸入庫存")]
        public int instock { get; set; }

        [Required(ErrorMessage = "請輸入圖片檔名")]
        public string image { get; set; }
    }
}