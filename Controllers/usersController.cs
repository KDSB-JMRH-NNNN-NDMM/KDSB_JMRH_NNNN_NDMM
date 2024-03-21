﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KDSB_JMRH_NNNN_NDMM.Models;

namespace KDSB_JMRH_NNNN_NDMM.Controllers
{
    public class usersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public usersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: users
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.users.Include(u => u.Role);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var users = await _context.users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: users/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.roles, "Id", "Id");
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,Email,Status,RoleId")] users users, IFormFile image)
        {
            if (ModelState.IsValid)
            {

                if (image != null && image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        users.Image = memoryStream.ToArray();

                    }
                }
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.roles, "Id", "Id", users.RoleId);
            return View(users);
        }

        // GET: users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var users = await _context.users.FindAsync(id);
            if (users == null)
            {
                return NotFound();

            }
            ViewData["RoleId"] = new SelectList(_context.roles, "Id", "Name", users.RoleId);
            return View(users);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,Email,Status,RoleId")] users users, IFormFile image)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            var registroFind = await _context.users.FirstOrDefaultAsync(s => s.Id == users.Id);

            if (registroFind == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null && image.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await image.CopyToAsync(memoryStream);
                            users.Image = memoryStream.ToArray();

                        }
                    }
                    else
                    {
                        // Conservar la imagen existente si no se proporciona una nueva
                        if (registroFind.Image?.Length > 0)
                        {
                            users.Image = registroFind.Image;
                        }
                    }

                    // Actualizar los otros campos
                    registroFind.UserName = users.UserName;
                    registroFind.Password = users.Password;
                    registroFind.Email = users.Email;
                    registroFind.Status = users.Status;
                    registroFind.RoleId = users.RoleId;

                    // Asignar la nueva imagen a la entidad antes de actualizar
                    registroFind.Image = users.Image;

                    _context.Update(registroFind);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!usersExists(users.Id))
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
            // Si ModelState no es válido, regresa a la vista con los datos del usuario
            ViewData["RoleId"] = new SelectList(_context.roles, "Id", "Name", users.RoleId);
            return View(users);
        }



        // GET: users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var users = await _context.users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.users'  is null.");
            }
            var users = await _context.users.FindAsync(id);
            if (users != null)
            {
                _context.users.Remove(users);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool usersExists(int id)
        {
          return _context.users.Any(e => e.Id == id);
        }
    }
}
