using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using pc.Models;




namespace pc.Service
{ 
    public class MemberService
    {

        private pcsysEntities db = new pcsysEntities();

        #region 註冊
        public void register(member mem)
        {
            //密碼加密
            mem.password = MD5(mem.password);
            db.member.AddObject(mem);
            db.SaveChanges();
        }
        #endregion

        #region 正式會員驗證
        public bool usernameValidate(string u, string a)
        {
            bool result = false;
            var Enum = db.member.AsEnumerable();
            var mem = Enum.Where(m => MD5(m.username) == u && m.authcode == a).FirstOrDefault();

            if (mem != null)
            {
                result = true;
                mem.authcode = null;
                db.SaveChanges();
            }
            return result;
        }

        #endregion

        #region 檢查帳號是否使用
       
        public bool check_username(string username)
        {
            bool check = false;
            //判斷是否已有帳號
            if (db.member.Where(m => m.username == username).FirstOrDefault() == null)
            {
                check = true;//可以使用
            }
            return check;

        }
        #endregion

        #region 帳號權限
        public string getRoles(string username)
        {
            string roles = "user";
            var mem = db.member.Where(u => u.username == username).FirstOrDefault();
            if (mem.role)
            {
                roles = "admin";
            }
            return roles;
        }
        #endregion

        #region 登入
        public string login(string username, string password)
        {
            string login_state = string.Empty;
            string encr_pw = MD5(password);
            member login = db.member.Where(m => m.username == username && m.password == encr_pw).FirstOrDefault();
            
            if (login != null)
            {
                if (login.authcode == null)
                {
                    login_state = "ok";
                }
                else
                {
                    login_state = "此帳號尚未通過驗證";
                }
            }
            else
            {
                login_state = "您輸入的資料有誤";
            }
            return login_state;
        }
        #endregion

        #region 修改密碼
        public string change_password(string username,string password,string new_password)
        {
            string message = string.Empty;
            member mem = db.member.Where(m => m.username == username).FirstOrDefault();
            if (mem != null)
            {
                if (mem.password.Equals(MD5(password)))
                {
                    mem.password = MD5(new_password);
                    db.SaveChanges();
                    message = "修改密碼成功!";
                }
                else
                {
                    message = "舊密碼輸入錯誤!";
                }
            }
            return message;
        }
        #endregion

        #region 取得會員資料
        public member getMemdata(string username)
        {
            member mem = db.member.Where(m => m.username == username).FirstOrDefault();
            return mem;

        }
        #endregion

        #region 修改會員資料
        public string change_memdata(string username,string name,string phone,string address,string email)
        {
            string message = string.Empty;
            member mem = db.member.Where(m => m.username == username).FirstOrDefault();
            if (mem != null)
            {
                mem.name = name;
                mem.phone = phone;
                mem.address = address;
                mem.email = email;
                db.SaveChanges();
                message = "修改成功!";
            }
            else
            {
                message = "修改失敗!";
            }
            return message;
        }
        #endregion

        #region 忘記密碼
        public string forget_password(string username, string email,string authcode)
        {
            string message = string.Empty;
            member mem = db.member.Where(m => m.username == username && m.email == email).FirstOrDefault();

            if (mem != null)
            {
                mem.authcode = authcode;
                db.SaveChanges();
                message = "ok";
            }
            else
            {
                message = "資料輸入有誤!";
            }

            return message;
        }
        #endregion

        #region 設置密碼//u=username,a=authcode
       
        public string set_password(string u, string a,string new_password)
        {
            string message = string.Empty;
            var dbs = db.member.AsEnumerable();
            var mem = dbs.Where(m => MD5(m.username) == u && m.authcode == a).FirstOrDefault();

            if (mem != null)
            {
                mem.password = MD5(new_password);
                mem.authcode = null;
                db.SaveChanges();
                message = "新密碼設定成功";

            }
            else
            {
                message = "新密碼設定失敗";
            }

            return message;
        }
        #endregion

        #region 表單驗證票據
        public void FormsAuthTicket(string username,string role, int expiration)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(expiration),
                        false,
                        role,
                        FormsAuthentication.FormsCookiePath);

            string encrypt = FormsAuthentication.Encrypt(ticket);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encrypt));
        }
        #endregion

        #region 取的會員id
        public int getUid(string username)
        {
            int id = 0;
            var mem = db.member.Where(m => m.username == username).FirstOrDefault();
            if (mem != null)
            {
                id = mem.id;
            }
            return id;
        }
        #endregion

        #region 加密
        public string MD5(string password)
        {
            string encr_pw = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            return encr_pw;
        }
        #endregion

        #region 取的會員訂單
        public List<order> getMemOrder(int uid)
        {
            
            var order = db.order.Where(o => o.uid == uid).OrderBy(o => o.id).ToList();
            return order;
        }
        #endregion
        public List<orderDetail> getMemOrderDetail(int oid)
        {
            var orderDetail = db.orderDetail.Where(o => o.orderId == oid).ToList();
            return orderDetail;

        }

        #region orderCancel 取消訂單
        public void myOrderCancel(int oid)
        {
            var orders = db.order.Where(m => m.id == oid).FirstOrDefault();
         
            if (orders != null)
            {
                db.order.DeleteObject(orders);
                db.SaveChanges();    
            }
 
        }
        #endregion

        #region orderCancel 取消訂單
        public void myOrderDetailCancel(int oid)
        {

            var orderDetails = db.orderDetail.Where(m => m.orderId == oid);
            if (orderDetails.FirstOrDefault() != null)
            {
                foreach (var detail in orderDetails)
                {
                    db.orderDetail.DeleteObject(detail);
                    db.SaveChanges(System.Data.Objects.SaveOptions.None);
                }
            }

        }
        #endregion
    }
}