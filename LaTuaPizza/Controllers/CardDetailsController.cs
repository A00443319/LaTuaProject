using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaTuaPizza.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace LaTuaPizza.Controllers
{
    public class CardDetailsController : Controller
    {
        private readonly _5510Context _context;
        private static readonly Random random = new Random();

        public CardDetailsController(_5510Context context)
        {
            _context = context;
        }

        /*
         *  Displays payment form
         * **/
        public IActionResult Index()
        {
            Customer user = GetSignedUser();
            ViewBag.userPhone = user.Phone;
            ViewBag.totalPrice = GetCartTotal();
            return View();
        }

        /*
         * Gets the signed in user
         * **/
        public Customer GetSignedUser()
        {
            return _context.Customer.Where(a => a.Email == HttpContext.Session.GetString("email")).FirstOrDefault();
        }

        /*
         * Get total price of the cart Items
         * **/
        public decimal? GetCartTotal()
        {
            List<Cart> cart = GetCart();
            decimal? menuTotal = (decimal?)0.0;
            foreach (Cart item in cart)
            {
                menuTotal += item.CartItem.Price * item.Qty;
            }
            return menuTotal;
        }


        /*
         * Reads the JSON and creates a Cart list.
         * **/
        public List<Cart> GetCart()
        {
            var cart = HttpContext.Session.GetString("cart");
            JArray json = JArray.Parse(cart);
            List<Cart> cartItems = new List<Cart>();
            foreach (JObject item in json)
            {
                foreach (KeyValuePair<string, JToken> property in item)
                {
                    int menuId = Convert.ToInt32(property.Key);
                    int quantity = Convert.ToInt32(property.Value);
                    Cart cartItem = new Cart
                    {
                        CartItem = _context.Menu.Where(m => m.MenuId == menuId).FirstOrDefault(),
                        Qty = quantity
                    };
                    cartItems.Add(cartItem);
                }
            }
            return cartItems;
        }

        /*
         * Validate and Save Card details
         * **/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidateAndSave(CardDetails cardDetails)
        {
            //Bind[("CardNo,CardName,Phone,Expiry,CardType")]
             if (ModelState.IsValid)
             {
                 _context.Add(cardDetails);
                 _context.SaveChangesAsync();
                 //return RedirectToAction(nameof(Index));
             }
            return RedirectToAction(nameof(CreateOrder));
            //return RedirectToAction(nameof(Index));
        }


        /*
         * Creates an order
         * **/
        public IActionResult CreateOrder()
        {
            // get phone, addr, card, status 
            var total = GetCartTotal().ToString();
            Customer user = GetSignedUser();
            Address address = _context.Address.Where(m => m.Phone == user.Phone).FirstOrDefault();
            CardDetails card = _context.CardDetails.Where(m => m.Phone == user.Phone).FirstOrDefault();
            OrdStatus status = _context.OrdStatus.Where(m => m.StatusId == 5).FirstOrDefault();

            if(address != null && card!= null && status != null)
            {
                OrderInfo order = new OrderInfo
                {
                    CreatedDate = DateTime.Now,
                    Phone = user.Phone,
                    StatusId = status.StatusId,
                    AddrId = address.AddrId,
                    CardNo = card.CardNo,
                    PriceBeforeTax = total,
                    PriceAfterTax = total,
                    PhoneNavigation = user,
                    CardNoNavigation = card,
                    Addr = address,
                    Status = status
                };

                order.CnfNo = GenerateConfirmationNumber();
                HttpContext.Session.SetString("confirmationNumber", order.CnfNo);
                //save the order 
                _context.Add(order);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(CreateMenuItem));   
        }

        /*
         * Method to save the number of items for the order
         * **/
        public IActionResult CreateMenuItem()
        {
            List<Cart> cart = GetCart();
            OrderInfo order = _context.OrderInfo.Where(m => m.CnfNo == HttpContext.Session.GetString("confirmationNumber")).FirstOrDefault();
            if (order != null)
            {
                foreach (Cart item in cart)
                {
                    MenuItem menuItem = new MenuItem
                    {
                        OrdId = order.OrdId,
                        MenuId = item.CartItem.MenuId,
                        Qty = item.Qty
                    };
                    _context.Add(menuItem);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "OrderInfo");
        }

        /*
         * Method to create order confirmation number
         * **/
        public static string GenerateConfirmationNumber()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        //------------------------------------------------------------------------------------------------------
        //SCAFFOLDED METHODS


        // GET: CardDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardDetails = await _context.CardDetails
                .Include(c => c.PhoneNavigation)
                .FirstOrDefaultAsync(m => m.CardNo == id);
            if (cardDetails == null)
            {
                return NotFound();
            }

            return View(cardDetails);
        }

        // GET: CardDetails/Create
        public IActionResult Create()
        {
            ViewData["Phone"] = new SelectList(_context.Customer, "Phone", "Fname");
            return View();
        }

        // POST: CardDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardNo,CardName,Phone,Expiry,CardType")] CardDetails cardDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Phone"] = new SelectList(_context.Customer, "Phone", "Fname", cardDetails.Phone);
            return View(cardDetails);
        }

        // GET: CardDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardDetails = await _context.CardDetails.FindAsync(id);
            if (cardDetails == null)
            {
                return NotFound();
            }
            ViewData["Phone"] = new SelectList(_context.Customer, "Phone", "Fname", cardDetails.Phone);
            return View(cardDetails);
        }

        // POST: CardDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CardNo,CardName,Phone,Expiry,CardType")] CardDetails cardDetails)
        {
            if (id != cardDetails.CardNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardDetailsExists((int)cardDetails.CardNo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Phone"] = new SelectList(_context.Customer, "Phone", "Fname", cardDetails.Phone);
            return View(cardDetails);
        }

        // GET: CardDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardDetails = await _context.CardDetails
                .Include(c => c.PhoneNavigation)
                .FirstOrDefaultAsync(m => m.CardNo == id);
            if (cardDetails == null)
            {
                return NotFound();
            }

            return View(cardDetails);
        }

        // POST: CardDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cardDetails = await _context.CardDetails.FindAsync(id);
            _context.CardDetails.Remove(cardDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardDetailsExists(int id)
        {
            return _context.CardDetails.Any(e => e.CardNo == id);
        }

        
    }
}
