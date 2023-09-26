using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD.Data;
using CRUD.Models;
using Microsoft.AspNetCore.Authorization;

namespace CRUD.Controllers
{
    [Authorize]
    public class crudsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public crudsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: cruds
        public async Task<IActionResult> Index()
        {
              return _context.crudProgram != null ? 
                          View(await _context.crudProgram.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.crudProgram'  is null.");
        }

        // GET: cruds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.crudProgram == null)
            {
                return NotFound();
            }

            var crud = await _context.crudProgram
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crud == null)
            {
                return NotFound();
            }

            return View(crud);
        }

        // GET: cruds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: cruds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email")] crud crud)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crud);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crud);
        }

        // GET: cruds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.crudProgram == null)
            {
                return NotFound();
            }

            var crud = await _context.crudProgram.FindAsync(id);
            if (crud == null)
            {
                return NotFound();
            }
            return View(crud);
        }

        // POST: cruds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email")] crud crud)
        {
            if (id != crud.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crud);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!crudExists(crud.Id))
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
            return View(crud);
        }

        // GET: cruds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.crudProgram == null)
            {
                return NotFound();
            }

            var crud = await _context.crudProgram
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crud == null)
            {
                return NotFound();
            }

            return View(crud);
        }

        // POST: cruds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.crudProgram == null)
            {
                return Problem("Entity set 'ApplicationDbContext.crudProgram'  is null.");
            }
            var crud = await _context.crudProgram.FindAsync(id);
            if (crud != null)
            {
                _context.crudProgram.Remove(crud);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool crudExists(int id)
        {
          return (_context.crudProgram?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
