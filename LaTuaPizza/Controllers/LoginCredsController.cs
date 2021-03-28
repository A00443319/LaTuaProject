using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaTuaPizza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LaTuaPizza.Controllers
{
    public class LoginCredsController : Controller
    {
        private readonly _5510Context _context;

        public LoginCredsController(_5510Context context)
        {
            _context = context;
        }

        // GET: LoginCreds
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoginCred.ToListAsync());
        }

        // GET: LoginCreds/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginCred = await _context.LoginCred
                .FirstOrDefaultAsync(m => m.Email == id);
            if (loginCred == null)
            {
                return NotFound();
            }

            return View(loginCred);
        }

        // GET: LoginCreds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoginCreds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Password")] LoginCred loginCred)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loginCred);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loginCred);
        }

        // GET: LoginCreds/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginCred = await _context.LoginCred.FindAsync(id);
            if (loginCred == null)
            {
                return NotFound();
            }
            return View(loginCred);
        }

        // POST: LoginCreds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email,Password")] LoginCred loginCred)
        {
            if (id != loginCred.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loginCred);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginCredExists(loginCred.Email))
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
            return View(loginCred);
        }

        // GET: LoginCreds/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginCred = await _context.LoginCred
                .FirstOrDefaultAsync(m => m.Email == id);
            if (loginCred == null)
            {
                return NotFound();
            }

            return View(loginCred);
        }

        // POST: LoginCreds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var loginCred = await _context.LoginCred.FindAsync(id);
            _context.LoginCred.Remove(loginCred);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginCredExists(string id)
        {
            return _context.LoginCred.Any(e => e.Email == id);
        }
    }
}
