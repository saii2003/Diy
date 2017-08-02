using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace pc.Models
{
    [Serializable]
    public class Cart
    {
        //放入所有商品
        public List<cartItem> cartItems;

        public Cart()
        {
            this.cartItems = new List<cartItem>();
        }

        

        //全部商品總價
        public int total
        {
            get 
            {
                var total = 0;
                foreach (var cartItem in this.cartItems)
                {
                    total += cartItem.one_total;
                    
                }
                return total;
            }
        }
        //全部商品總數
        public int count
        {
            get
            {
                return this.cartItems.Count;
            }
        }

        public bool add_product(string cpu, string cpu_amount, string cpu_price, string mainboard, string mainboard_amount, string mainboard_price, string memory, string memory_amount, string memory_price, string hardware, string hardware_amount, string hardware_price, string videocard, string videocard_amount, string videocard_price, string power, string power_amount, string power_price, string cases, string cases_amount, string cases_price, string dvd, string dvd_amount, string dvd_price, string monitor, string monitor_amount, string monitor_price)
        {
           
            ArrayList shopcart = new ArrayList();
            if ((cpu != "" && cpu_amount != "" && cpu_price != "") || (mainboard != null && mainboard_amount != null && mainboard_price != null) || (memory != null && memory_amount != null && memory_price != null) || (hardware != null && hardware_amount != null && hardware_price != null) || (videocard != null && videocard_amount != null && videocard_price != null) || (power != null && power_amount != null && power_price != null) || (power != null && power_amount != null && power_price != null) || (dvd != null && dvd_amount != null && dvd_price != null) || (monitor != null && monitor_amount != null && monitor_price != null))
            {
                if (cpu != null && cpu_amount != null && cpu_price != null)
                {
                    shopcart.Add(cpu + "," + cpu_amount + "," + cpu_price);
                }
                if (mainboard != null && mainboard_amount != null && mainboard_price != null)
                {
                    shopcart.Add(mainboard + "," + mainboard_amount + "," + mainboard_price);
                }
                if (memory != null && memory_amount != null && memory_price != null)
                {
                    shopcart.Add(memory + "," + memory_amount + "," + memory_price);
                }
                if (hardware != null && hardware_amount != null && hardware_price != null)
                {
                    shopcart.Add(hardware + "," + hardware_amount + "," + hardware_price);
                }
                if (videocard != null && videocard_amount != null && videocard_price != null)
                {
                    shopcart.Add(videocard + "," + videocard_amount + "," + videocard_price);
                }
                if (power != null && power_amount != null && power_price != null)
                {
                    shopcart.Add(power + "," + power_amount + "," + power_price);
                }
                if (cases != null && cases_amount != null && cases_price != null)
                {
                    shopcart.Add(cases + "," + cases_amount + "," + cases_price);
                }
                if (dvd != null && dvd_amount != null && dvd_price != null)
                {
                    shopcart.Add(dvd + "," + dvd_amount + "," + dvd_price);
                }
                if (monitor != null && monitor_amount != null && monitor_price != null)
                {
                    shopcart.Add(monitor + "," + monitor_amount + "," + monitor_price);
                }

                for (int i = 0; i < shopcart.Count; i++)
                {
                    var item = cartItems.Where(p => p.name == shopcart[i].ToString().Split(',')[0]).Select(p => p).FirstOrDefault();

                    if (item == null)
                    {
                        
                        
                            var cartitem = new cartItem()
                            {
                                name = shopcart[i].ToString().Split(',')[0],
                                amount = Convert.ToInt32(shopcart[i].ToString().Split(',')[1]),
                                price = Convert.ToInt32(shopcart[i].ToString().Split(',')[2]),
                            };
                            this.cartItems.Add(cartitem);
                        

                    }
                    else
                    {
                        if (item.amount < 10)
                        {
                            item.amount += 1;
                        }
                    }
                }
            }
            return true;
        }
        public bool clearCart()
        {
            cartItems.Clear();
            return true;
        }

        public bool remove_product(string name)
        {

            var item = cartItems.Where(p => p.name == name).Select(p=>p).FirstOrDefault();

            if (item == null)
            {
            }
            else
            {
                cartItems.Remove(item);
            }
            return true;
        }

    }
}