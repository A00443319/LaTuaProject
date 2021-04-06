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
    public class CardDetailsController : Controller
    {
        private readonly _5510Context _context;

        public CardDetailsController(_5510Context context)
        {
            _context = context;
        }

        // GET: CardDetails
        public async Task<IActionResult> Index()
        {
            var _5510Context = _context.CardDetails.Include(c => c.PhoneNavigation);
            return View(await _5510Context.ToListAsync());
        }

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
                    if (!CardDetailsExists(cardDetails.CardNo))
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
