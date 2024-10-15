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
    public class testsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public testsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: tests
        public async Task<IActionResult> Index()
        {
              return _context.test != null ? 
                          View(await _context.test.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.test'  is null.");
        }

        // GET: tests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.test == null)
            {
                return NotFound();
            }

            var test = await _context.test
                .FirstOrDefaultAsync(m => m.Id == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // GET: tests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: tests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,testString")] test test)
        {
            if (ModelState.IsValid)
            {
                _context.Add(test);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(test);
        }

        // GET: tests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.test == null)
            {
                return NotFound();
            }

            var test = await _context.test.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            return View(test);
        }

        // POST: tests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,testString")] test test)
        {
            if (id != test.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(test);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!testExists(test.Id))
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
            return View(test);
        }

        // GET: tests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.test == null)
            {
                return NotFound();
            }

            var test = await _context.test
                .FirstOrDefaultAsync(m => m.Id == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.test == null)
            {
                return Problem("Entity set 'ApplicationDbContext.test'  is null.");
            }
            var test = await _context.test.FindAsync(id);
            if (test != null)
            {
                _context.test.Remove(test);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool testExists(int id)
        {
          return (_context.test?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
