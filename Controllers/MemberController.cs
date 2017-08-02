using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using pc.Service;
using pc.Models;

namespace pc.Controllers
{
    public class MemberController : Controller
    {
        private MemberService memService = new MemberService();
        private MailService mailserv = new MailService();
        private pcsysEntities db = new pcsysEntities();

        #region 註冊
        public ActionResult register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Pc");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult register(MemberRegView memRegView)
        {
            string validateCode = mailserv.authcode();

            if (ModelState.IsValid)
            {
                memRegView.members.password = memRegView.password;
                memRegView.members.authcode = validateCode;
                memService.register(memRegView.members);
                
                //會員驗證超連結
                UriBuilder validateUrl = new UriBuilder(Request.Url)
                {
                    Path = Url.Action("uVali", "Member", new { u = memService.MD5(memRegView.members.username), a = validateCode })
                };
                mailserv.send_mail(memRegView.members.email, "會員驗證信", "請點以下的連結進行會員驗證<br/><a href=\"" + validateUrl + "\">" + validateUrl + "</a>");

                TempData["reg_state"] = "註冊成功，驗證信已寄送您的信箱";
                return RedirectToAction("Regresult");

            }
            return View();
        }
        #endregion

        #region 註冊結果
        public ActionResult regresult()
        {
            if (TempData["reg_state"] == null)
            {
                return RedirectToAction("Index", "Pc");
            }
            return View();
        }
        #endregion

        #region 檢查帳號是否使用
        public JsonResult checkusername(MemberRegView mem)
        {
            
            return Json(memService.check_username(mem.members.username), JsonRequestBehavior.AllowGet);
              
        }
        #endregion

        #region 正式會員驗證
        public ActionResult uVali(string u,string a)
        {
            if (u != null && a != null)
            {
                if (memService.usernameValidate(u, a))
                {
                    ViewData["validateResult"] = "驗證成功，已經成為正式會員";
                }
                else
                {
                    ViewData["validateResult"] = "帳號已經驗證過";
                }
            }
            else
            {
                return RedirectToAction("Index", "Pc");
            }
            return View();
        }
        #endregion

        #region 登入
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Pc");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Login(MemberLoginView memLoginView)
        {
            if (ModelState.IsValid)
            {
                //保持登入30天
                if (memLoginView.rememberMe == true)
                {
                    //確認驗證碼
                    if (memLoginView.validatecode.Trim().ToLower().Equals(Session["valicode"].ToString().ToLower()))
                    {
                        //登入驗證
                        if (memService.login(memLoginView.username, memLoginView.password) == "ok")
                        {
                            memService.FormsAuthTicket(memLoginView.username, memService.getRoles(memLoginView.username), 43200);//保持登入30天
                            return RedirectToAction("Index", "Pc");
                        }
                        else
                        {
                            ModelState.AddModelError("", memService.login(memLoginView.username, memLoginView.password));
                            return View();
                        }
                    }
                    else
                    {
                            ModelState.AddModelError("", "驗證碼有誤");
                            return View();
                    }
                }
                else
                {
                    if (memLoginView.validatecode.Trim().ToLower().Equals(Session["valicode"].ToString().ToLower()))
                    {
                        if (memService.login(memLoginView.username, memLoginView.password) == "ok")
                        {
                            memService.FormsAuthTicket(memLoginView.username, memService.getRoles(memLoginView.username), 60);//60分鐘登入
                            return RedirectToAction("Index", "Pc");
                        }
                        else
                        {
                            ModelState.AddModelError("", memService.login(memLoginView.username, memLoginView.password));
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "驗證碼有誤");
                        return View();
                    }
                }

            }
            return View(memLoginView);
           
        }
        #endregion

        #region Logout 登出
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Member");
        }
        #endregion

        #region 修改密碼
        [Authorize]
        public ActionResult changepw()
        {
            return View();
        } 
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult changepw(ChangePwView change)
        {
            if (ModelState.IsValid)
            {
                ViewData["result"] = memService.change_password(User.Identity.Name, change.password, change.newpassword);
            }
            return View();
        }
        #endregion

        #region 修改個人資料
        [Authorize]
        public ActionResult Changemem()
        {
            if (User.Identity.IsAuthenticated)
            {
                member mem = memService.getMemdata(User.Identity.Name);
                if (mem != null)
                {
                    ViewData["name"] = mem.name;
                    ViewData["phone"] = mem.phone;
                    ViewData["address"] = mem.address;
                    ViewData["email"] = mem.email;
                }
            }
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Changemem(ChangeMemView change)
        {
            if (ModelState.IsValid)
            {
                ViewData["result"] = memService.change_memdata(User.Identity.Name, change.name, change.phone, change.address, change.email);
                return RedirectToAction("Changemem", "Member");
            }
            return View();
        }
        #endregion

        #region 忘記密碼
        public ActionResult forgetpw()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Pc");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult forgetpw(ForgetPwView fpv)
        {
            if (ModelState.IsValid)
            {
                string validateCode = mailserv.authcode();
                if (memService.forget_password(fpv.username, fpv.email, validateCode) == "ok")
                {
                    UriBuilder url = new UriBuilder(Request.Url)
                    {
                        Path = Url.Action("Setpw", "Member", new { u = memService.MD5(fpv.username), a = validateCode })
                    };
                    mailserv.send_mail(fpv.email, "忘記密碼-重新設定新密碼", "請點選下面連結設定新密碼<br/> <a href=\"" + url + "\">" + url + "</a>");
                    TempData["forgetresult"] = "設定新密碼已傳送到" + fpv.email.Replace(fpv.email.Substring(3, 5), "*****") + "信箱中。";
                    return RedirectToAction("foresult");
                }
            }

            return View();
        }
        #endregion

        #region 忘記密碼結果
        public ActionResult foresult()
        {
            if (TempData["forgetresult"] == null)
            {
                return RedirectToAction("Index", "Pc");
            }
            return View();
        }
        #endregion

        #region forgetpw設置新密碼
        public ActionResult setpw(string u,string a)
        {
            if (u == null && a == null)
            {
                return RedirectToAction("Index", "Pc");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult setpw(string u,string a, SetPwView spv)
        {
            if (ModelState.IsValid)
            {
                ViewData["result"] = memService.set_password(u, a, spv.new_password);
            }
            return View();
        }
        #endregion

        #region 登入頁面驗證碼
        public ActionResult valicode()
        {
            validateCode code = new validateCode();
            code.validate_code();
            return View();
        }
        #endregion

        #region 會員訂單
        [Authorize]
        public ActionResult myOrder()
        {
            var uid = memService.getUid(User.Identity.Name);
            myOrderView myOrder = new myOrderView();
            myOrder.order = memService.getMemOrder(uid);
            return View(myOrder);
        }
        #endregion

        #region 訂單明細
        [Authorize]
        public ActionResult OrderDetail_Partial(int oid)
        {
            myOrderView myOrder = new myOrderView();
            myOrder.orderDetail = memService.getMemOrderDetail(oid);
            return PartialView(myOrder);
        }
        #endregion

        #region 取消訂單
        [Authorize]
        [HttpPost]
        public ActionResult orderCancel(int oid)
        {
            if (oid.ToString() != null)
            {
                memService.myOrderCancel(oid);
                memService.myOrderDetailCancel(oid);
                return RedirectToAction("myOrder", "Member");
                
            }
            else
            {
                return RedirectToAction("myOrder", "Member");
            }
   
        }
       
        #endregion

        


    }
}
