using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckUser([Bind("Email,Password")] LoginCred loginCred)
        {
            if (ModelState.IsValid)
            {
                LoginCred user = _context.LoginCred.Where(a => a.Email == loginCred.Email && a.Password == loginCred.Password).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Email/Password");
                    return View("Index",loginCred);
                }
            }
            //Go to Menu page
            return Redirect("/Menus");
        }
    }
}
