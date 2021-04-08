using LaTuaPizza.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTuaPizza.Controllers
{
    public class CartController : Controller
    {

        private readonly _5510Context _context;

        public CartController(_5510Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userEmail = HttpContext.Session.GetString("email");
            var cart = HttpContext.Session.GetString("cart");
            JArray json = JArray.Parse(cart);
            
            List<Cart> cartItems = new List<Cart>();

            //Deserializing Json array.
            //Get Menu details based on 
            foreach(JObject item in json)
            {
                foreach (KeyValuePair<string, JToken> property in item)
                {
                    int menuId =  Convert.ToInt32(property.Key);
                    int quantity = Convert.ToInt32(property.Value);
                    Cart cartItem = new Cart();
                    cartItem.CartItem = _context.Menu.Where(m => m.MenuId == menuId).FirstOrDefault();
                    cartItem.Qty = quantity;
                    cartItems.Add(cartItem);
                }
            }
            ViewBag.cartItems = cartItems;
            ViewBag.totalItems = json.Count;
            return View();
        }

        public IActionResult ProcessCart()
        {

            return RedirectToAction("Index", "CardDetails");
        }
    }
}
