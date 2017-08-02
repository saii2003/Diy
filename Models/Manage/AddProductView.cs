using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages.Html;

namespace pc.Models
{
    public class AddProductView
    {
        public product products { get; set; }
        
        [DisplayName("名稱")]
        [Required(ErrorMessage = "請輸入商品名稱")]
        public string name { get; set; }

        [DisplayName("類別")]
        [Required(ErrorMessage = "請選擇商品分類")]
        public int cid { get; set; }
        
        [DisplayName("價格")]
        [Required(ErrorMessage = "請輸入商品價格")]
        public int price { get; set; }

        [DisplayName("")]
        [Required(ErrorMessage = "請輸入商品圖片")]
        public HttpPostedFileBase image { get; set; }

        [DisplayName("名稱")]
        [Required(ErrorMessage = "請輸入商品庫存")]
        public int instock { get; set; }
        
  

        


        
        
    }
}