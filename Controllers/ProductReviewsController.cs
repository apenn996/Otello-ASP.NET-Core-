using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Otello.Data;
using Otello.Models;

namespace Otello.Controllers
{
    public class ProductReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductReviews
        public async Task<IActionResult> Index()
        {
              return _context.ProductReviews != null ? 
                          View(await _context.ProductReviews.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ProductReviews'  is null.");
        }

        // GET: ProductReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductReviews == null)
            {
                return NotFound();
            }

            var productReviews = await _context.ProductReviews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productReviews == null)
            {
                return NotFound();
            }

            return View(productReviews);
        }

        // GET: ProductReviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Name,Message,Rating")] ProductReviews productReviews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productReviews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productReviews);
        }
        [HttpPost]
        

			// GET: ProductReviews/Edit/5
			public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductReviews == null)
            {
                return NotFound();
            }

            var productReviews = await _context.ProductReviews.FindAsync(id);
            if (productReviews == null)
            {
                return NotFound();
            }
            return View(productReviews);
        }

        // POST: ProductReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Name,Message,Rating")] ProductReviews productReviews)
        {
            if (id != productReviews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productReviews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductReviewsExists(productReviews.Id))
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
            return View(productReviews);
        }

        // GET: ProductReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductReviews == null)
            {
                return NotFound();
            }

            var productReviews = await _context.ProductReviews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productReviews == null)
            {
                return NotFound();
            }

            return View(productReviews);
        }

        // POST: ProductReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id2)
        {
			System.Diagnostics.Debug.WriteLine("In DELETE 1: " + id2 );
			if (_context.ProductReviews == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductReviews'  is null.");
            }
            var productReviews = _context.ProductReviews.FirstOrDefault(s => s.Id == id2);
			System.Diagnostics.Debug.WriteLine("In DELETE 2: " + id2 + " / " + productReviews);
			var test = productReviews.ProductId;
			
			if (productReviews != null)
            {
                _context.ProductReviews.Remove(productReviews);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("ProductDisplay", "Products", new {id=productReviews.ProductId});
        }

        private bool ProductReviewsExists(int id)
        {
          return (_context.ProductReviews?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
