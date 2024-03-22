using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KDSB_JMRH_NNNN_NDMM.Models;
using Microsoft.AspNetCore.Authorization;

namespace KDSB_JMRH_NNNN_NDMM.Controllers
{
    [Authorize]
    public class rolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public rolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: roles
        public async Task<IActionResult> Index(string searchString)
        {
            var users = from u in _context.roles
                        select u;

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.Name.Contains(searchString));
            }

            return View(await users.ToListAsync());
        }

        // GET: roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.roles == null)
            {
                return NotFound();
            }

            var roles = await _context.roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }

        // GET: roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] roles roles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roles);
        }

        // GET: roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.roles == null)
            {
                return NotFound();
            }

            var roles = await _context.roles.FindAsync(id);
            if (roles == null)
            {
                return NotFound();
            }
            return View(roles);
        }

        // POST: roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] roles roles)
        {
            if (id != roles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!rolesExists(roles.Id))
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
            return View(roles);
        }

        // GET: roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.roles == null)
            {
                return NotFound();
            }

            var roles = await _context.roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }

        // POST: roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.roles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.roles'  is null.");
            }
            var roles = await _context.roles.FindAsync(id);
            if (roles != null)
            {
                _context.roles.Remove(roles);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool rolesExists(int id)
        {
          return _context.roles.Any(e => e.Id == id);
        }
    }
}
