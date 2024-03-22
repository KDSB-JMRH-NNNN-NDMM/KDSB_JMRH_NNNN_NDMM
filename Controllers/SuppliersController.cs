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
    public class SuppliersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuppliersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index(string searchString)
        {
            var users = from u in _context.suppliers
                        select u;

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.Name.Contains(searchString));
            }

            return View(await users.ToListAsync());
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.suppliers == null)
            {
                return NotFound();
            }

            var suppliers = await _context.suppliers
                .Include(s => s.PhoneNumber)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suppliers == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Details";
            return View(suppliers);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            var suppliers = new Suppliers();
            suppliers.PhoneNumber = new List<PhoneNumbers>();
            
            ViewBag.Accion = "Create";
            return View(suppliers);
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Address,PhoneNumber")] Suppliers suppliers)
        {
            
                _context.Add(suppliers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            
        }
        [HttpPost]
        public ActionResult AgregarDetalles([Bind("Id,Name,Email,Address,PhoneNumber")] Suppliers suppliers, string accion)
        {
            suppliers.PhoneNumber.Add(new PhoneNumbers { });
            ViewBag.Accion = accion;
            return View(accion, suppliers);
        }
        public ActionResult EliminarDetalles([Bind("Id,Name,Email,Address,PhoneNumber")] Suppliers suppliers,
           int index, string accion)
        {
            var det = suppliers.PhoneNumber[index];
            if (accion == "Edit" && det.Id > 0)
            {
                det.Id = det.Id * -1;
            }
            else
            {
                suppliers.PhoneNumber.RemoveAt(index);
            }

            ViewBag.Accion = accion;
            return View(accion, suppliers);
        }
        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.suppliers == null)
            {
                return NotFound();
            }

            var suppliers = await _context.suppliers
                .Include(s => s.PhoneNumber)
                .FirstAsync(s => s.Id == id);

            if (suppliers == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Edit";
            return View(suppliers);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Address,PhoneNumber")] Suppliers suppliers)
        {
            if (id != suppliers.Id)
            {
                return NotFound();
            }

            try
            {
                // Obtener los datos de la base de datos que van a ser modificados
                var facturaUpdate = await _context.suppliers
                        .Include(s => s.PhoneNumber)
                        .FirstAsync(s => s.Id == suppliers.Id);
                facturaUpdate.Name = suppliers.Name;
                facturaUpdate.Email = suppliers.Email;
                facturaUpdate.Address = suppliers.Address;
                // Obtener todos los detalles que seran nuevos y agregarlos a la base de datos
                var detNew = suppliers.PhoneNumber.Where(s => s.Id == 0);
                foreach (var d in detNew)
                {
                    facturaUpdate.PhoneNumber.Add(d);
                }
                // Obtener todos los detalles que seran modificados y actualizar a la base de datos
                var detUpdate = suppliers.PhoneNumber.Where(s => s.Id > 0);
                foreach (var d in detUpdate)
                {
                    var det = facturaUpdate.PhoneNumber.FirstOrDefault(s => s.Id == d.Id);
                    det.PhoneNumber = d.PhoneNumber;
                    
                    det.Note = d.Note;
                }
                // Obtener todos los detalles que seran eliminados y actualizar a la base de datos
                var delDet = suppliers.PhoneNumber.Where(s => s.Id < 0).ToList();
                if (delDet != null && delDet.Count > 0)
                {
                    foreach (var d in delDet)
                    {
                        d.Id = d.Id * -1;
                        var det = facturaUpdate.PhoneNumber.FirstOrDefault(s => s.Id == d.Id);
                        _context.Remove(det);
                        // facturaUpdate.DetFacturaVenta.Remove(det);
                    }
                }
                // Aplicar esos cambios a la base de datos
                _context.Update(facturaUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuppliersExists(suppliers.Id))
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

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.suppliers == null)
            {
                return NotFound();
            }

            var suppliers = await _context.suppliers
                .Include(s => s.PhoneNumber)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suppliers == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Delete";
            return View(suppliers);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.suppliers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.suppliers'  is null.");
            }
            var suppliers = await _context.suppliers.FindAsync(id);
            if (suppliers != null)
            {
                _context.suppliers.Remove(suppliers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuppliersExists(int id)
        {
          return _context.suppliers.Any(e => e.Id == id);
        }
    }
}
