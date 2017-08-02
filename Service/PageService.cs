using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pc.Service
{
    public class PageService
    {
        //目前頁數
        public int currentPage { get; set; }
        //最大頁數
        public int MaxPage { get; set; }
        //每頁資料數10筆
        public int itemCount
        {
            get { return 10; }
        }
        //預設目前頁數為1
        public PageService()
        {
            this.currentPage = 1;
        }

        public PageService(int page)
        {
            this.currentPage = page;
        }

        public void rightPage()
        {
            if (this.currentPage < 1)
            {
                this.currentPage = 1;
            }
            else if (this.currentPage > this.MaxPage)
            {
                this.currentPage = this.MaxPage;
            }
            if (this.MaxPage.Equals(0))
            {
                this.currentPage = 1;
            }
        }
    }
}