using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;


namespace pc.Service
{
    public class validateCode
    {
        public void validate_code()
        {
            Random rd = new Random();
            string code = string.Empty;

            string[] str = {
                              "A","B","C","D","E","F","G","H","I","J","K","L","M",
                              "N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
                              "a","b","c","d","e","f","g","h","i","j","k","l","m",
                              "n","o","p","q","r","s","t","u","v","w","x","y","z",
                              "0","1","2","3","4","5","6","7","8","9",
                            };

            for (int i = 0; i < 5; i++)
            {
                code += str[rd.Next(str.Count())];
            }
            
            int width = (int)(code.Length*20);//設定圖像寬
            Bitmap image = new Bitmap(width, 45);
            Graphics g = Graphics.FromImage(image);
            //背景顏色
            g.Clear(Color.White);

            Font font = new Font("Arial", 16, FontStyle.Bold);
            Point point1 = new Point(rd.Next(0, 5), rd.Next(0, 20));
            Point point2 = new Point(rd.Next(0, 5), rd.Next(0, 30));
            Brush brush = new LinearGradientBrush(point1, point2, Color.Blue, Color.Red);
            Pen pen = new Pen(new SolidBrush(Color.Gray), 1);
            
            g.DrawString(code, font, brush, 10, 10);
            
            //隨機畫線
            for (int i = 0; i < 20; i++)
            {
              
                int x0 = rd.Next(0, image.Width);
                int y0 = rd.Next(0, image.Height);
                int x1 = rd.Next(0, image.Width);
                int y1 = rd.Next(0, image.Height);
                g.DrawLine(pen, x0, y0, x1, x1);
            }

            HttpContext.Current.Session["valicode"] = code;
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);//將影像儲存記憶體中
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Jpeg";
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());


            g.Dispose();
            image.Dispose();
        }
    }
}