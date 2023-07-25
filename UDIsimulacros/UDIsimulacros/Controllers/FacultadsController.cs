using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UDIsimulacros.models;

namespace UDIsimulacros.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class FacultadsController : Controller
    {
        private readonly DbsimulacrosudiContext _context;

        public FacultadsController(DbsimulacrosudiContext context)
        {
            _context = context;
        }

        // GET: Facultads
        public async Task<IActionResult> Index()
        {
              return View(await _context.Facultads.ToListAsync());
        }

        // GET: Facultads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Facultads == null)
            {
                return NotFound();
            }

            var facultad = await _context.Facultads
                .FirstOrDefaultAsync(m => m.Idfacultad == id);
            if (facultad == null)
            {
                return NotFound();
            }

            return View(facultad);
        }

        // GET: Facultads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facultads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idfacultad,Nombre")] Facultad facultad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facultad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facultad);
        }

        // GET: Facultads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Facultads == null)
            {
                return NotFound();
            }

            var facultad = await _context.Facultads.FindAsync(id);
            if (facultad == null)
            {
                return NotFound();
            }
            return View(facultad);
        }

        // POST: Facultads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idfacultad,Nombre")] Facultad facultad)
        {
            if (id != facultad.Idfacultad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facultad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultadExists(facultad.Idfacultad))
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
            return View(facultad);
        }

        // GET: Facultads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Facultads == null)
            {
                return NotFound();
            }

            var facultad = await _context.Facultads
                .FirstOrDefaultAsync(m => m.Idfacultad == id);
            if (facultad == null)
            {
                return NotFound();
            }

            return View(facultad);
        }

        // POST: Facultads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Facultads == null)
            {
                return Problem("Entity set 'DbsimulacrosudiContext.Facultads'  is null.");
            }
            var facultad = await _context.Facultads.FindAsync(id);
            if (facultad != null)
            {
                _context.Facultads.Remove(facultad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultadExists(int id)
        {
          return _context.Facultads.Any(e => e.Idfacultad == id);
        }
    }
}
