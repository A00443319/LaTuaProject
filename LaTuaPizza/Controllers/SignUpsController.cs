using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaTuaPizza.Models;

namespace LaTuaPizza.Controllers
{
    public class SignUpsController : Controller
    {
        private readonly _5510Context _context;

        public SignUpsController(_5510Context context)
        {
            _context = context;
        }

        // GET: SignUps
        public IActionResult Index()
        {
            return View();
        }

        // GET: SignUps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signUp = await _context.SignUps
                .FirstOrDefaultAsync(m => m.SignupId == id);
            if (signUp == null)
            {
                return NotFound();
            }

            return View(signUp);
        }

        // GET: SignUps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SignUps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Register register)
        {
            if (ModelState.IsValid)
            {
                LoginCred cred = register.Credentials;
                _context.Add(cred);
                _context.SaveChanges();
                register.User.EmailNavigation = _context.LoginCred.Where(a => a.Email == cred.Email).FirstOrDefault();
                _context.Add(register.User);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else {
                //ModelState.AddModelError(string.Empty, "Invalid Phone");
                return View("Index", register);
            }
            
        }

        // GET: SignUps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signUp = await _context.SignUps.FindAsync(id);
            if (signUp == null)
            {
                return NotFound();
            }
            return View(signUp);
        }

        // POST: SignUps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SignupId,FirstName,LastName,Email,PhoneNumber,Password,ConfirmPass,City,Province,PostalCode")] SignUp signUp)
        {
            if (id != signUp.SignupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(signUp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SignUpExists(signUp.SignupId))
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
            return View(signUp);
        }

        // GET: SignUps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signUp = await _context.SignUps
                .FirstOrDefaultAsync(m => m.SignupId == id);
            if (signUp == null)
            {
                return NotFound();
            }

            return View(signUp);
        }

        // POST: SignUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var signUp = await _context.SignUps.FindAsync(id);
            _context.SignUps.Remove(signUp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SignUpExists(int id)
        {
            return _context.SignUps.Any(e => e.SignupId == id);
        }
    }
}
