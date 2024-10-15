using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Otello.Data;
using Otello.Models;

namespace Otello.Controllers
{
    public class ProductColorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductColorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductColors
        public async Task<IActionResult> Index()
        {
              return _context.ProductColors != null ? 
                          View(await _context.ProductColors.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ProductColors'  is null.");
        }

        // GET: ProductColors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductColors == null)
            {
                return NotFound();
            }

            var productColors = await _context.ProductColors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productColors == null)
            {
                return NotFound();
            }

            return View(productColors);
        }

        // GET: ProductColors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Color")] ProductColors productColors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productColors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productColors);
        }

        // GET: ProductColors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductColors == null)
            {
                return NotFound();
            }

            var productColors = await _context.ProductColors.FindAsync(id);
            if (productColors == null)
            {
                return NotFound();
            }
            return View(productColors);
        }

        // POST: ProductColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Color")] ProductColors productColors)
        {
            if (id != productColors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productColors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductColorsExists(productColors.Id))
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
            return View(productColors);
        }

        // GET: ProductColors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductColors == null)
            {
                return NotFound();
            }

            var productColors = await _context.ProductColors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productColors == null)
            {
                return NotFound();
            }

            return View(productColors);
        }

        // POST: ProductColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductColors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductColors'  is null.");
            }
            var productColors = await _context.ProductColors.FindAsync(id);
            if (productColors != null)
            {
                _context.ProductColors.Remove(productColors);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductColorsExists(int id)
        {
          return (_context.ProductColors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
