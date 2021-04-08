using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTuaPizza.Models
{
    public class Cart
    {
        public Cart()
        {
            CartItem = new Menu();
        }
        public Menu CartItem { get; set; }
        public int Qty { get; set; }
    }

}
