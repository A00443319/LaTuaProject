using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using LaTuaPizza.Models;

namespace LaTuaPizza.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly _5510Context _context;

        public HomeController(ILogger<HomeController> logger, _5510Context context)
        {
            _logger = logger;
            _context = context;
        }

        /*
         * Displays the Login form
         * **/
        public IActionResult Index()
        {
            return View();
        }

        /*
         * Signs out the user
         * **/
        public IActionResult UserSignOut()
        {
            //deletes the stored session variables
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /*
         * Method to validate user's credentials.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckUser([Bind("Email,Password")] LoginCred loginCred)
        {
            if (ModelState.IsValid)
            {
                //check user email and password
                LoginCred user = _context.LoginCred.Where(a => a.Email == loginCred.Email && a.Password == loginCred.Password).FirstOrDefault();
                if (user == null)
                {
                    //throw error message
                    ModelState.AddModelError(string.Empty, "Invalid Email/Password");
                    return View("Index",loginCred);
                }
                HttpContext.Session.SetString("isSignedIn", "true");
            }
            //Go to Menu page
            HttpContext.Session.SetString("email", loginCred.Email);
            return RedirectToAction("Index","Menus");
        }
    }
}
