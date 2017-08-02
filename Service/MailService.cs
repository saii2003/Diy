using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Security;


namespace pc.Service
{
    public class MailService
    {
        #region 寄信
        public void send_mail(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("ggogopro@gmail.com");
            mail.To.Add(to);

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("ggogopro@gmail.com", "i'4namaaeljo3xjp6");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
        #endregion

        #region 驗證碼
        public string authcode()
        {
            string authcode = string.Empty;

            string[] code = {"0","1","2","3","4","5","6","7","8","9",
                              "A","B","C","D","E","F","G","H","I","J","K","L","M",
                              "N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
                              "a","b","c","d","e","f","g","h","i","j","k","l","m",
                              "n","o","p","q","r","s","t","u","v","w","x","y","z",
                            };

            Random rd = new Random();
            for (int i = 0; i < 12; i++)
            {
                authcode += code[rd.Next(code.Count())];
            }
            return authcode;
        }
        #endregion
    }
}