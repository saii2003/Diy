using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using pc.Models;
using System.Web.Mvc;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using System.Collections;



namespace pc.Service
{
    public class PCService
    {
        pcsysEntities db = new pcsysEntities();

        #region Index.cshtml Dropdownlist商品項目
        public List<SelectListItem> pc_item(int cid)
        {

            var pc = db.product.Where(p => p.cid == cid).ToList();
            List<SelectListItem> product_item = new List<SelectListItem>();
            foreach (var pc_items in pc)
            {
                product_item.Add(new SelectListItem()
                {
                    Text = pc_items.name + ",$" + pc_items.price.ToString("#.#"),
                    Value = pc_items.name + "," + pc_items.price.ToString("#.#")
                });
            }
            return product_item;
        }
        #endregion

        #region Index.cshtml 商品項目總數
        public int item_count(int cid)
        {
            return db.product.Where(p => p.cid == cid).ToList().Count();
        }

        #endregion

        #region Index.cshtml 取的商品圖片
        public string getProductImage()
        {
            var product = db.product.Where(p => p.name == "Intel Celeron G4400【雙核】3.3GHZ/54W").ToList();
            return product.FirstOrDefault().image;
        }
        #endregion
        #region list.所有商品
        public List <product> getAllProduct(PageService page)
        {

            IQueryable<product> data = db.product;
            //總頁數
            page.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(data.Count()) / page.itemCount));
            //校正頁碼
            page.rightPage();
            return data.OrderBy(p => p.id).Skip((page.currentPage - 1) * page.itemCount).Take(page.itemCount).ToList();

        }
        #endregion

        #region list.搜尋商品
        public List<product> searchProduct(PageService page, ProductListView list)
        {
            var data = db.product.AsEnumerable().Where(p => p.cid.ToString().Contains(list.cid.ToString()) && p.name.Contains(list.search));
            page.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(data.Count()) / page.itemCount));
            page.rightPage();

            return data.OrderBy(p => p.id).Skip((page.currentPage - 1) * page.itemCount).Take(page.itemCount).ToList();
        }
        #endregion

        #region update.更新商品
        public void updateProduct(int id,UpdateProductView updates)
        {
            var product = db.product.Where(p=>p.id == id).FirstOrDefault();
            product.name = updates.name;
            product.cid = updates.cid;
            product.price = updates.price;
            product.instock = updates.instock;
            product.image = updates.image;

            db.SaveChanges();     
        }
        #endregion

        #region update.取得商品資料
        public product getProduct(int id)
        {
            var product = db.product.Where(p => p.id == id).FirstOrDefault();
            return product;
        }
        #endregion

        

        #region delete.刪除商品
        public void delete(int id)
        {
            var delete_product = db.product.Where(p => p.id == id).FirstOrDefault();
            db.product.DeleteObject(delete_product);
            db.SaveChanges();   
        }
        #endregion

        #region edit.取得分類category
        public List<SelectListItem> getCategory(int cid)
        {
            var categorys = db.catogory.ToList();//取得所有分類

            List<SelectListItem> item = new List<SelectListItem>();
            foreach(var category in categorys)
            {
                item.Add(new SelectListItem()
                {
                    Text = category.c_name.ToString(),
                    Value = category.id.ToString(),
                    Selected = category.id.Equals(cid)
                });
            }
            return item;
        }

        #endregion

        #region add.DropdownList商品分類
        public List<SelectListItem> categoryItem()
        {

            var categorys = db.catogory.ToList();
            List<SelectListItem> item = new List<SelectListItem>();

            foreach (var category in categorys)
            {
                item.Add(new SelectListItem()
                {
                    Text = category.c_name.ToString(),
                    Value = category.id.ToString()
                });
            }
            return item;
            
        }
        #endregion

        #region 增加商品
        public void productAdd(string name, int cid, int price, int instock,HttpPostedFileBase file)
        {
            product products = new product();
            products.name = name;
            products.cid = cid;
            products.price = price;
            products.instock = instock;
            products.image = Path.GetFileName(file.FileName);
            db.product.AddObject(products);
            db.SaveChanges();

        }
        #endregion

        #region 上傳圖片
        public void upload_file(HttpPostedFileBase image)
        {    
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Image"), image.FileName);
            image.SaveAs(path);
        }
        #endregion

        #region 允許上傳副檔名格式
        public bool allow_fileExtension(HttpPostedFileBase image)
        {
            bool fileAllow = false;
            //副檔名格式gif, jpg, png
            string[] allowExtension = { ".gif", ".jpg", ".png" };

            if (image.ContentLength > 0)
            {
                string fileExtension = Path.GetExtension(image.FileName).ToLower();

                for (int i = 0; i < allowExtension.Length; i++)
                {
                    if (fileExtension == allowExtension[i])
                    {
                        fileAllow = true;
                    }
                }
            }
            return fileAllow;
        }
        #endregion

        #region 匯出Excel
        public void ExportToExcel(string[,] product)
        {
            
            var dt = new DataTable("估價單");
            dt.Columns.Add("編號", typeof(string));
            dt.Columns.Add("商品名稱", typeof(string));
            dt.Columns.Add("數量", typeof(string));
            dt.Columns.Add("單價", typeof(string));
            dt.Columns.Add("小計", typeof(int));

            int all_total = 0;//計算全部商品總價
            int id = 1;
            for (int i = 0; i < 9; i++)
            {
                int one_total = 0;//計算單樣商品總價
                if (!string.IsNullOrEmpty(product[i, 0]) && !string.IsNullOrEmpty(product[i, 1]) && !string.IsNullOrEmpty(product[i, 2]))
                {
                    one_total = Convert.ToInt32(product[i, 1]) * Convert.ToInt32(product[i, 2]);
                    dt.Rows.Add(id, product[i, 0], product[i, 2], product[i, 1], one_total);
                    id++;

                }

                all_total += one_total;
            }
            dt.Rows.Add("", "", "", "總計", all_total);

            var grid = new GridView();
            grid.DataSource = dt;
            grid.DataBind();

            HttpContext.Current.Response.ClearContent();//清除緩衝區所有內容
            HttpContext.Current.Response.Buffer = true;//將資料寫入緩衝區，資料處理完再送出
            HttpContext.Current.Response.Charset = "BIG5";
            HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charst=BIG5>");
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=估價單.xls");
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("BIG5");			
            HttpContext.Current.Response.ContentType = "application/excel";
			
			//將資料寫入gridview中再輸出資料流
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);//輸出到HtmlTextWriter物件
			
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();

        }
        #endregion

        #region 儲存訂單
        public void saveOrder(ConfirmView confirm, int uid, int price)
        {
            var order = new order();
            order.uid = uid;
            order.receiver = confirm.receiver;
            order.phone = confirm.phone;
            order.address = confirm.address;
            order.price = price;
            order.payment = false;
            order.shipment = false;
            db.order.AddObject(order);
            db.SaveChanges();

            var orderDetail = new orderDetail();
            foreach (var item in cartOperation.GetCurrentCart().cartItems)
            {
                orderDetail.orderId = order.id;
                orderDetail.name = item.name;
                orderDetail.amount = item.amount;
                orderDetail.price = item.price;
                db.orderDetail.AddObject(orderDetail);
                db.SaveChanges(System.Data.Objects.SaveOptions.None);
              
            } 
        }
        #endregion

        


    }
}