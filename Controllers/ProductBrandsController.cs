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
    public class ProductBrandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductBrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductBrands
        public async Task<IActionResult> Index()
        {
              return _context.ProductBrands != null ? 
                          View(await _context.ProductBrands.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ProductBrands'  is null.");
        }

        // GET: ProductBrands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductBrands == null)
            {
                return NotFound();
            }

            var productBrands = await _context.ProductBrands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productBrands == null)
            {
                return NotFound();
            }

            return View(productBrands);
        }

        // GET: ProductBrands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrandName,BrandSite,BrandImage")] ProductBrands productBrands)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productBrands);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productBrands);
        }

        // GET: ProductBrands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductBrands == null)
            {
                return NotFound();
            }

            var productBrands = await _context.ProductBrands.FindAsync(id);
            if (productBrands == null)
            {
                return NotFound();
            }
            return View(productBrands);
        }

        // POST: ProductBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrandName,BrandSite,BrandImage")] ProductBrands productBrands)
        {
            if (id != productBrands.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productBrands);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductBrandsExists(productBrands.Id))
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
            return View(productBrands);
        }

        // GET: ProductBrands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductBrands == null)
            {
                return NotFound();
            }

            var productBrands = await _context.ProductBrands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productBrands == null)
            {
                return NotFound();
            }

            return View(productBrands);
        }

        // POST: ProductBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductBrands == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductBrands'  is null.");
            }
            var productBrands = await _context.ProductBrands.FindAsync(id);
            if (productBrands != null)
            {
                _context.ProductBrands.Remove(productBrands);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductBrandsExists(int id)
        {
          return (_context.ProductBrands?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
