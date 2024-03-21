using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KDSB_JMRH_NNNN_NDMM.Models;

namespace KDSB_JMRH_NNNN_NDMM.Controllers
{
    public class PhoneNumbersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhoneNumbersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PhoneNumbers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.phoneNumbers.Include(p => p.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PhoneNumbers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.phoneNumbers == null)
            {
                return NotFound();
            }

            var phoneNumbers = await _context.phoneNumbers
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phoneNumbers == null)
            {
                return NotFound();
            }

            return View(phoneNumbers);
        }

        // GET: PhoneNumbers/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.suppliers, "Id", "Id");
            return View();
        }

        // POST: PhoneNumbers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SupplierId,PhoneNumber,Note")] PhoneNumbers phoneNumbers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phoneNumbers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.suppliers, "Id", "Id", phoneNumbers.SupplierId);
            return View(phoneNumbers);
        }

        // GET: PhoneNumbers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.phoneNumbers == null)
            {
                return NotFound();
            }

            var phoneNumbers = await _context.phoneNumbers.FindAsync(id);
            if (phoneNumbers == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.suppliers, "Id", "Id", phoneNumbers.SupplierId);
            return View(phoneNumbers);
        }

        // POST: PhoneNumbers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SupplierId,PhoneNumber,Note")] PhoneNumbers phoneNumbers)
        {
            if (id != phoneNumbers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phoneNumbers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneNumbersExists(phoneNumbers.Id))
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
            ViewData["SupplierId"] = new SelectList(_context.suppliers, "Id", "Id", phoneNumbers.SupplierId);
            return View(phoneNumbers);
        }

        // GET: PhoneNumbers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.phoneNumbers == null)
            {
                return NotFound();
            }

            var phoneNumbers = await _context.phoneNumbers
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phoneNumbers == null)
            {
                return NotFound();
            }

            return View(phoneNumbers);
        }

        // POST: PhoneNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.phoneNumbers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.phoneNumbers'  is null.");
            }
            var phoneNumbers = await _context.phoneNumbers.FindAsync(id);
            if (phoneNumbers != null)
            {
                _context.phoneNumbers.Remove(phoneNumbers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneNumbersExists(int id)
        {
          return _context.phoneNumbers.Any(e => e.Id == id);
        }
    }
}
