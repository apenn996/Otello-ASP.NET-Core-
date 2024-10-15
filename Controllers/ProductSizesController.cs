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
    public class ProductSizesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductSizesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductSizes
        public async Task<IActionResult> Index()
        {
              return _context.ProductSizes != null ? 
                          View(await _context.ProductSizes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ProductSizes'  is null.");
        }

        // GET: ProductSizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductSizes == null)
            {
                return NotFound();
            }

            var productSizes = await _context.ProductSizes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSizes == null)
            {
                return NotFound();
            }

            return View(productSizes);
        }

        // GET: ProductSizes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductSizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Size")] ProductSizes productSizes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productSizes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productSizes);
        }

        // GET: ProductSizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductSizes == null)
            {
                return NotFound();
            }

            var productSizes = await _context.ProductSizes.FindAsync(id);
            if (productSizes == null)
            {
                return NotFound();
            }
            return View(productSizes);
        }

        // POST: ProductSizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Size")] ProductSizes productSizes)
        {
            if (id != productSizes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productSizes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSizesExists(productSizes.Id))
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
            return View(productSizes);
        }

        // GET: ProductSizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductSizes == null)
            {
                return NotFound();
            }

            var productSizes = await _context.ProductSizes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSizes == null)
            {
                return NotFound();
            }

            return View(productSizes);
        }

        // POST: ProductSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductSizes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductSizes'  is null.");
            }
            var productSizes = await _context.ProductSizes.FindAsync(id);
            if (productSizes != null)
            {
                _context.ProductSizes.Remove(productSizes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSizesExists(int id)
        {
          return (_context.ProductSizes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
