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
    public class OrderInfoController : Controller
    {
        private readonly _5510Context _context;

        public OrderInfoController(_5510Context context)
        {
            _context = context;
        }

        // GET: OrderInfo
        public async Task<IActionResult> Index()
        {
            var _5510Context = _context.OrderInfo.Include(o => o.Addr).Include(o => o.CardNoNavigation).Include(o => o.PhoneNavigation).Include(o => o.Status);
            return View(await _5510Context.ToListAsync());
        }

        // GET: OrderInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderInfo = await _context.OrderInfo
                .Include(o => o.Addr)
                .Include(o => o.CardNoNavigation)
                .Include(o => o.PhoneNavigation)
                .Include(o => o.Status)
                .FirstOrDefaultAsync(m => m.OrdId == id);
            if (orderInfo == null)
            {
                return NotFound();
            }

            return View(orderInfo);
        }

        // GET: OrderInfo/Create
        public IActionResult Create()
        {
            ViewData["AddrId"] = new SelectList(_context.Address, "AddrId", "City");
            ViewData["CardNo"] = new SelectList(_context.CardDetails, "CardNo", "CardName");
            ViewData["Phone"] = new SelectList(_context.Customer, "Phone", "Fname");
            ViewData["StatusId"] = new SelectList(_context.OrdStatus, "StatusId", "StatusName");
            return View();
        }

        // POST: OrderInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdId,CnfNo,CreatedDate,Phone,AddrId,CardNo,StatusId,PriceBeforeTax,PriceAfterTax")] OrderInfo orderInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddrId"] = new SelectList(_context.Address, "AddrId", "City", orderInfo.AddrId);
            ViewData["CardNo"] = new SelectList(_context.CardDetails, "CardNo", "CardName", orderInfo.CardNo);
            ViewData["Phone"] = new SelectList(_context.Customer, "Phone", "Fname", orderInfo.Phone);
            ViewData["StatusId"] = new SelectList(_context.OrdStatus, "StatusId", "StatusName", orderInfo.StatusId);
            return View(orderInfo);
        }

        // GET: OrderInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderInfo = await _context.OrderInfo.FindAsync(id);
            if (orderInfo == null)
            {
                return NotFound();
            }
            ViewData["AddrId"] = new SelectList(_context.Address, "AddrId", "City", orderInfo.AddrId);
            ViewData["CardNo"] = new SelectList(_context.CardDetails, "CardNo", "CardName", orderInfo.CardNo);
            ViewData["Phone"] = new SelectList(_context.Customer, "Phone", "Fname", orderInfo.Phone);
            ViewData["StatusId"] = new SelectList(_context.OrdStatus, "StatusId", "StatusName", orderInfo.StatusId);
            return View(orderInfo);
        }

        // POST: OrderInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrdId,CnfNo,CreatedDate,Phone,AddrId,CardNo,StatusId,PriceBeforeTax,PriceAfterTax")] OrderInfo orderInfo)
        {
            if (id != orderInfo.OrdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderInfoExists(orderInfo.OrdId))
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
            ViewData["AddrId"] = new SelectList(_context.Address, "AddrId", "City", orderInfo.AddrId);
            ViewData["CardNo"] = new SelectList(_context.CardDetails, "CardNo", "CardName", orderInfo.CardNo);
            ViewData["Phone"] = new SelectList(_context.Customer, "Phone", "Fname", orderInfo.Phone);
            ViewData["StatusId"] = new SelectList(_context.OrdStatus, "StatusId", "StatusName", orderInfo.StatusId);
            return View(orderInfo);
        }

        // GET: OrderInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderInfo = await _context.OrderInfo
                .Include(o => o.Addr)
                .Include(o => o.CardNoNavigation)
                .Include(o => o.PhoneNavigation)
                .Include(o => o.Status)
                .FirstOrDefaultAsync(m => m.OrdId == id);
            if (orderInfo == null)
            {
                return NotFound();
            }

            return View(orderInfo);
        }

        // POST: OrderInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderInfo = await _context.OrderInfo.FindAsync(id);
            _context.OrderInfo.Remove(orderInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderInfoExists(int id)
        {
            return _context.OrderInfo.Any(e => e.OrdId == id);
        }
    }
}
