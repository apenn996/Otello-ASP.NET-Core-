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
    public class ProductVariationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductVariationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductVariations
        public async Task<IActionResult> Index()
        {
              return _context.ProductVariations != null ? 
                          View(await _context.ProductVariations.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ProductVariations'  is null.");
        }

        // GET: ProductVariations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductVariations == null)
            {
                return NotFound();
            }

            var productVariations = await _context.ProductVariations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productVariations == null)
            {
                return NotFound();
            }

            return View(productVariations);
        }

        // GET: ProductVariations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductVariations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,productId,Color,Size")] ProductVariations productVariations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productVariations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productVariations);
        }

        // GET: ProductVariations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductVariations == null)
            {
                return NotFound();
            }

            var productVariations = await _context.ProductVariations.FindAsync(id);
            if (productVariations == null)
            {
                return NotFound();
            }
            return View(productVariations);
        }

        // POST: ProductVariations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,productId,Color,Size")] ProductVariations productVariations)
        {
            if (id != productVariations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productVariations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductVariationsExists(productVariations.Id))
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
            return View(productVariations);
        }

        // GET: ProductVariations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductVariations == null)
            {
                return NotFound();
            }

            var productVariations = await _context.ProductVariations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productVariations == null)
            {
                return NotFound();
            }

            return View(productVariations);
        }

        // POST: ProductVariations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductVariations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductVariations'  is null.");
            }
            var productVariations = await _context.ProductVariations.FindAsync(id);
            if (productVariations != null)
            {
                _context.ProductVariations.Remove(productVariations);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductVariationsExists(int id)
        {
          return (_context.ProductVariations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
