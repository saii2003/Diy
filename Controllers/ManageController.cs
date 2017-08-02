using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pc.Models;
using pc.Service;
using pc.Models.Manage;

namespace pc.Controllers
{

    [Authorize(Roles="admin")]
    public class ManageController : Controller
    {
        PCService pcServer = new PCService();
        pcsysEntities db = new pcsysEntities();

        #region 增加商品
        public ActionResult add()
        {
            //dropdownlist商品分類
            ViewBag.categroy = pcServer.categoryItem();
            if (ViewBag.categroy == null)
            {
                return RedirectToAction("list", "Manage");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add(string name, int cid, int price, int instock, HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
            {
                if (pcServer.allow_fileExtension(file))//判斷副檔名是否為jpg,gif,png
                {

                    product products = new product();
                    products.name = name;
                    products.cid = cid;
                    products.price = price;
                    products.instock = instock;
                    products.image = System.IO.Path.GetFileName(file.FileName);
                    db.product.AddObject(products);
                    db.SaveChanges();

                    pcServer.upload_file(file);//上傳圖片
                    return RedirectToAction("list", "Manage");
                }
                else
                {
                    return JavaScript("alert('只允許\"gif\",\"jpg\",\"png\"檔案格式')");
                }
            }
            else
            {
                ModelState.AddModelError("", "請選擇上傳檔案");
            }

            return View();
        }
        #endregion

        #region 商品清單
        public ActionResult list(string sort, ProductListView productListView, int page = 1)
        {   
            //dropdownlist商品分類
            ViewBag.category = pcServer.categoryItem();
            ViewBag.productCount = db.product.Count();
            productListView.page = new PageService(page);

            //判斷搜尋是否有輸入字串
            if (productListView.cid == null || productListView.search == null)
            {
                //顯示資料排序
                ViewBag.pid = sort == "pid" ? "pid_desc" : "pid";
                ViewBag.pName = sort == "pName" ? "pName_desc" : "pName";
                ViewBag.pPrice = sort == "pPrice" ? "pPrice_desc" : "pPrice";
                ViewBag.pStock = sort == "pStock" ? "pStock_desc" : "pStock"; ;


                IQueryable<product> data = db.product;
                productListView.page.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(data.Count()) / productListView.page.itemCount));//最大頁數
                productListView.dataList = data.OrderBy(p => p.id).Skip((productListView.page.currentPage - 1) * productListView.page.itemCount).Take(productListView.page.itemCount).ToList();
                productListView.page.rightPage();//頁碼校正
                switch (sort)
                {
                    case "pid":
                        productListView.dataList = data.OrderBy(p => p.id).Skip((productListView.page.currentPage - 1) * productListView.page.itemCount).Take(productListView.page.itemCount).ToList();
                        break;
                    case "pid_desc":
                        productListView.dataList = data.OrderByDescending(p => p.id).Skip((productListView.page.currentPage - 1) * productListView.page.itemCount).Take(productListView.page.itemCount).ToList();
                        break;
                    case "pName":
                        productListView.dataList = data.OrderBy(p => p.name).Skip((productListView.page.currentPage - 1) * productListView.page.itemCount).Take(productListView.page.itemCount).ToList();
                        break;
                    case "pName_desc":
                        productListView.dataList = data.OrderByDescending(p => p.name).Skip((productListView.page.currentPage - 1) * productListView.page.itemCount).Take(productListView.page.itemCount).ToList();
                        break;
                    case "pPrice":
                        productListView.dataList = data.OrderBy(p => p.price).Skip((productListView.page.currentPage - 1) * productListView.page.itemCount).Take(productListView.page.itemCount).ToList();
                        break;
                    case "pPrice_desc":
                        productListView.dataList = data.OrderByDescending(p => p.price).Skip((productListView.page.currentPage - 1) * productListView.page.itemCount).Take(productListView.page.itemCount).ToList();
                        break;
                    case "pStock":
                        productListView.dataList = data.OrderBy(p => p.instock).Skip((productListView.page.currentPage - 1) * productListView.page.itemCount).Take(productListView.page.itemCount).ToList();
                        break;
                    case "pStock_desc":
                        productListView.dataList = data.OrderByDescending(p => p.instock).Skip((productListView.page.currentPage - 1) * productListView.page.itemCount).Take(productListView.page.itemCount).ToList();
                        break;
                }
            }
            else
            {
                //顯示所查詢資料
                var data = db.product.AsEnumerable().Where(p => p.cid.ToString().Contains(productListView.cid.ToString()) && p.name.Contains(productListView.search));
                productListView.page.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(data.Count()) / productListView.page.itemCount));//最大頁數
                productListView.page.rightPage();//頁碼校正
                productListView.dataList = data.OrderBy(p => p.id).Skip((productListView.page.currentPage - 1) * productListView.page.itemCount).Take(productListView.page.itemCount).ToList();
            }
            return View(productListView);
        }
        #endregion

        #region edit.更新商品資料
        public ActionResult edit(int id,string page)
        {
            UpdateProductView update = new UpdateProductView();
            update.previousUrl = Request.UrlReferrer.AbsoluteUri.ToString();//取得前一頁url
           
            if (!string.IsNullOrEmpty(id.ToString()) && pcServer.getProduct(id) != null)
            {
                var product = db.product.Where(p => p.id == id).FirstOrDefault();
                ViewData["name"] = product.name;
                ViewData["price"] = product.price.ToString("#.#");
                ViewData["instock"] = product.instock;
                ViewData["image"] = product.image;
                ViewBag.category = pcServer.getCategory(product.cid);
                return View(update);
            }
            else
            {
                return RedirectToAction("list", "Manage", new { page = page });
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit(int id, UpdateProductView update)
        {
            
            if (ModelState.IsValid)
            {
                var product = db.product.Where(p => p.id == id).FirstOrDefault();
                product.name = update.name;
                product.cid = update.cid;
                product.price = update.price;
                product.instock = update.instock;
                product.image = update.image;

                db.SaveChanges();
                return Redirect(update.previousUrl);
            }
            return View();

        }
        #endregion

        #region 刪除商品
        public ActionResult delete(int id)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                var product = db.product.Where(p => p.id == id).FirstOrDefault();
                db.product.DeleteObject(product);
                db.SaveChanges();   
                return RedirectToAction("list", "Manage");
            }
            else
            {
                return RedirectToAction("Index", "Pc");
            }

        }
        #endregion

        #region 訂單管理頁面
        public ActionResult order(ordersView ordersView)
        {

            ordersView.order = db.order.ToList();
            List<SelectListItem> item = new List<SelectListItem>();
            item.Add(new SelectListItem() { Text = "請選擇查詢項目", Value = "", Selected = true });
            item.Add(new SelectListItem() { Text = "顯示所有訂單", Value = "1" });
            item.Add(new SelectListItem() { Text = "訂單編號", Value = "2" });
            item.Add(new SelectListItem() { Text = "收件者", Value = "3" });
            ViewBag.category = item;


            if (ordersView.category != "" && ordersView.searchStr != null)
            {
                switch (ordersView.category.ToString())
                {
                    case "2":
                        ordersView.order = db.order.AsEnumerable().Where(m => m.id.ToString().Contains(ordersView.searchStr)).ToList();
                        break;
                    case "3":
                        ordersView.order = db.order.Where(m => m.receiver.Contains(ordersView.searchStr)).ToList();
                        break;
                }
            }
            else
            {

                    ordersView.order = db.order.ToList();
                
            }
            

            return View(ordersView);
        }
        #endregion
        #region 編輯訂單
        public ActionResult orderEdit(int oid)
        {
            var order = db.order.Where(o => o.id == oid).FirstOrDefault();

            if (order != null)
            {
                ViewData["id"] = order.id;
                ViewData["uid"] = order.uid;
                ViewData["receiver"] = order.receiver;
                ViewData["phone"] = order.phone;
                ViewData["address"] = order.address;
                ViewData["price"] =Convert.ToInt32(order.price);
                ViewData["payment"] = order.payment;
                ViewData["shipment"] = order.shipment;
            }
            else
            {
                return RedirectToAction("order", "Manage");
            }


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult orderEdit(orderEditView orderEdit,int oid)
        {
            if (ModelState.IsValid)
            {
                var order = db.order.Where(o => o.id == oid).FirstOrDefault();
                if (order != null)
                {
                    order.receiver = orderEdit.receiver;
                    order.phone = orderEdit.phone;
                    order.address = orderEdit.address;
                    order.price = orderEdit.price;
                    order.payment = orderEdit.payment;
                    order.shipment = orderEdit.shipment;
                    db.SaveChanges();
                    return RedirectToAction("order", "Manage");
                }
            }
            return View();
        }
        #endregion



    }
}
