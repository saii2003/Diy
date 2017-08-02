using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pc.Service;
using System.IO;
using pc.Models;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using System.Collections;


namespace pc.Controllers
{
  
    public class PcController : Controller
    {
        MemberService memserv = new MemberService();
        PCService pcserv = new PCService();

        #region 首頁
        [AllowAnonymous]
        public ActionResult Index()
        {


            //dropdownlist 清單項目
            //顯示單一產品數量
            ViewBag.CPU = pcserv.pc_item(1);
            ViewData["CPU_Count"] = pcserv.item_count(1);


            ViewBag.mainboard = pcserv.pc_item(2);
            ViewData["mainboard_Count"] = pcserv.item_count(2);

            ViewBag.memory = pcserv.pc_item(3);
            ViewData["memory_Count"] = pcserv.item_count(3);

            ViewBag.hardware = pcserv.pc_item(4);
            ViewData["hardware_Count"] = pcserv.item_count(4);

            ViewBag.videocard = pcserv.pc_item(5);
            ViewData["videocard_Count"] = pcserv.item_count(5);

            ViewBag.power = pcserv.pc_item(6);
            ViewData["power_Count"] = pcserv.item_count(6);

            ViewBag.cases = pcserv.pc_item(7);
            ViewData["cases_Count"] = pcserv.item_count(7);

            ViewBag.dvd = pcserv.pc_item(8);
            ViewData["dvd_Count"] = pcserv.item_count(8);

            ViewBag.monitor = pcserv.pc_item(9);
            ViewData["monitor_Count"] = pcserv.item_count(9);

            return View();
        }
        #endregion

        #region 匯出Excel檔
        [AllowAnonymous]
        public ActionResult toExcel(string cpu, string cpu_amount, string mainboard, string mainboard_amount, string memory, string memory_amount, string hardware, string hardware_amount, string videocard, string videocard_amount, string power, string power_amount, string cases, string cases_amount, string dvd, string dvd_amount,string monitor,string monitor_amount)
        {

            string[] cpu_selectedValue = Server.HtmlDecode(cpu).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] mainboard_selectedValue = Server.HtmlDecode(mainboard).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] memory_selectedValue = Server.HtmlDecode(memory).Split(',');
            string[] hardware_selectedValue = Server.HtmlDecode(hardware).Split(',');
            string[] videocard_selectedValue = Server.HtmlDecode(videocard).Split(',');
            string[] power_selectedValue = Server.HtmlDecode(power).Split(',');
            string[] cases_selectedValue = Server.HtmlDecode(cases).Split(',');
            string[] dvd_selectedValue = Server.HtmlDecode(dvd).Split(',');
            string[] monitor_selectedValue = Server.HtmlDecode(monitor).Split(',');

            string[,] product = new string[9,3];
            if ((cpu != "" && cpu_amount != "") || (mainboard != "" && mainboard_amount != "") || (memory != "" && memory_amount != "") || (hardware != "" && hardware_amount != "") || (videocard != "" && videocard_amount != "") || (power != "" && power_amount != "") || (cases != "" && cases_amount != "") || (dvd != "" && dvd_amount != "") || (monitor != "" && monitor_amount != ""))
            {
                if (cpu != "" && cpu_amount != "")
                {
                    product[0, 0] = cpu_selectedValue[0];
                    product[0, 1] = cpu_selectedValue[1];
                    product[0, 2] = cpu_amount;
                }
                if (mainboard != "" && mainboard_amount != "")
                {
                    product[1, 0] = mainboard_selectedValue[0];
                    product[1, 1] = mainboard_selectedValue[1];
                    product[1, 2] = mainboard_amount;
                }
                if (memory != "" && memory_amount != "")
                {
                    product[2, 0] = memory_selectedValue[0];
                    product[2, 1] = memory_selectedValue[1];
                    product[2, 2] = memory_amount;
                }
                if (hardware != "" && hardware_amount != "")
                {
                    product[3, 0] = hardware_selectedValue[0];
                    product[3, 1] = hardware_selectedValue[1];
                    product[3, 2] = hardware_amount;
                }
                if (videocard != "" && videocard_amount != "")
                {
                    product[4, 0] = videocard_selectedValue[0];
                    product[4, 1] = videocard_selectedValue[1];
                    product[4, 2] = videocard_amount;
                }
                if (power != "" && power_amount != "")
                {
                    product[5, 0] = power_selectedValue[0];
                    product[5, 1] = power_selectedValue[1];
                    product[5, 2] = power_amount;
                }
                if (cases != "" && cases_amount != "")
                {
                    product[6, 0] = cases_selectedValue[0];
                    product[6, 1] = cases_selectedValue[1];
                    product[6, 2] = cases_amount;
                }
                if (dvd != "" && dvd_amount != "")
                {
                    product[7, 0] = dvd_selectedValue[0];
                    product[7, 1] = dvd_selectedValue[1];
                    product[7, 2] = dvd_amount;
                }
                if (monitor != "" && monitor_amount != "")
                {
                    product[8, 0] = monitor_selectedValue[0];
                    product[8, 1] = monitor_selectedValue[1];
                    product[8, 2] = monitor_amount;
                }
                pcserv.ExportToExcel(product);
            }
            else
            {
                return RedirectToAction("Index", "Pc");
            }
            return View();
        }
        #endregion

        #region 加入購物車
        public ActionResult addtoCart(string cpu, string cpu_amount, string cpu_price, string mainboard, string mainboard_amount, string mainboard_price, string memory, string memory_amount, string memory_price, string hardware, string hardware_amount, string hardware_price, string videocard, string videocard_amount, string videocard_price, string power, string power_amount, string power_price, string cases, string cases_amount, string cases_price, string dvd, string dvd_amount, string dvd_price, string monitor, string monitor_amount, string monitor_price)
        {
            var currentCart = cartOperation.GetCurrentCart();
            currentCart.add_product(cpu, cpu_amount, cpu_price, mainboard, mainboard_amount, mainboard_price, memory, memory_amount, memory_price, hardware, hardware_amount, hardware_price, videocard, videocard_amount, videocard_price, power, power_amount, power_price, cases, cases_amount, cases_price, dvd, dvd_amount, dvd_price, monitor, monitor_amount, monitor_price);
            return RedirectToAction("Index", "Pc");
        }
        #endregion

        #region 取消購物車單一商品
        public ActionResult removeCartItem(string name)
        {
            var currentCart = cartOperation.GetCurrentCart();
            currentCart.remove_product(name);
            return PartialView("Cart_Partial");
        }
        #endregion

        #region 清除購物車所有商品
        public ActionResult clearCart()
        {
            var currentCart = cartOperation.GetCurrentCart();
            currentCart.clearCart();
            return PartialView("Cart_Partial");
        }
        #endregion

        #region 結帳
        [Authorize]
        public ActionResult confirm()
        {
            if (cartOperation.GetCurrentCart().count != 0)
            {
                if (User.Identity.IsAuthenticated)
                {
                    member mem = memserv.getMemdata(User.Identity.Name);
                    if (mem != null)
                    {
                        ViewData["name"] = mem.name;
                        ViewData["phone"] = mem.phone;
                        ViewData["address"] = mem.address;
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Pc");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult confirm(ConfirmView confirm)
        {
           
            if (ModelState.IsValid)
            {
                var currentCart = cartOperation.GetCurrentCart();
                int uid = memserv.getUid(User.Identity.Name);

                pcserv.saveOrder(confirm, uid, currentCart.total);
                currentCart.clearCart();
                TempData["result"] = "購買成功";
                return RedirectToAction("shopok", "pc");

            }
            return View();

            
        }
        
        #endregion

        [Authorize]
        public ActionResult shopok()
        {
            
            return View();
        }




       




     

    }
}
