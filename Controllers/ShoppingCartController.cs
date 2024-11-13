using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Otello.Data;
using Otello.Models;


namespace Otello.Controllers
{
    [Route("/[controller]/[action]")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext db;

        public ShoppingCartController(ApplicationDbContext context)
        {
            db = context;
        }

        [Authorize]
        // GET: ShoppingCart
        public async Task<IActionResult> Index()
        {
			ViewBag.Pe = db.ProductVariations.ToList();
			ViewBag.ProductModel = db.Product.ToList();
			ViewBag.ColorModel = db.ProductColors.ToList();
			ViewBag.SizeModel = db.ProductSizes.ToList();
			ViewBag.ProductImagesModel = db.ProductImages.ToList();
            ViewBag.ShoppingCartModel = db.ShoppingCart.ToList();
            return View();
        }

        [Authorize]
        public async Task<IActionResult> QuickAddToCart(int cartItemId, double totalCost)
        {
           // colorid, sizeid, variation id
           var notVariantProductinImages = db.ProductImages.FirstOrDefault(a => a.ProductId == cartItemId && a.variant == false);
            System.Diagnostics.Debug.WriteLine("OMGOMGOMGOMGOGOGMGOGMGOGMGOMGOMG: " + notVariantProductinImages.Id);
            var notVariantProductinVariation = db.ProductVariations.FirstOrDefault(s => s.productId == notVariantProductinImages.ProductId && s.colorId == notVariantProductinImages.colorId);
            var variationId = notVariantProductinVariation.Id;
            var userId = User.Identity?.Name;
            bool dbOperationSuccess = false;
           
            //adds a row to the shopping cart table
            ShoppingCart cartEntry = new ShoppingCart(userId, 1, variationId, totalCost);
            db.ShoppingCart.Add(cartEntry);

            //decrements the stock for the product item's table
            var decrementProduct = db.Product.FirstOrDefault(s => s.Id == cartItemId);
            decrementProduct.Stock -= 1;
            db.Entry(decrementProduct).State = EntityState.Modified;


            if (db.SaveChanges() > 0)
            {
                dbOperationSuccess = true;
            }



            ViewBag.ShoppingCartModel = db.ShoppingCart.ToList();
            ViewBag.ProductModel = db.Product.ToList();
            ViewBag.ProductImagesModel = db.ProductImages.ToList();



            return RedirectToAction("Index", "Home");

        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int cartItemId, int quantity, int variationId, double totalCost, int cId, int sId, int vId)
        {
           
            var userId = User.Identity?.Name;
            bool dbOperationSuccess = false;
			System.Diagnostics.Debug.WriteLine("3" + cartItemId + cId + sId + vId);
			//adds a row to the shopping cart table
			ShoppingCart cartEntry = new ShoppingCart(userId, quantity, variationId, totalCost);
            db.ShoppingCart.Add(cartEntry);
            
            //decrements the stock for the product item's table
            var decrementProduct = db.Product.FirstOrDefault(s=>s.Id == cartItemId);
            decrementProduct.Stock-= quantity;
            db.Entry(decrementProduct).State = EntityState.Modified;

			
			if ( db.SaveChanges() > 0)
            {              
                dbOperationSuccess = true;
            }
			


			ViewBag.ShoppingCartModel = db.ShoppingCart.ToList();
            ViewBag.ProductModel = db.Product.ToList();
            ViewBag.ProductImagesModel = db.ProductImages.ToList();

			

			return RedirectToAction("onSubmit", "Products", new { id=cartItemId, c2Id=cId, s2Id=sId, v2Id=vId, success=dbOperationSuccess});

        }
        // GET: ShoppingCart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.ShoppingCart == null)
            {
                return NotFound();
            }

            var shoppingCart = await db.ShoppingCart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        // GET: ShoppingCart/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Quantity,ProductId,TotalCost")] ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                db.Add(shoppingCart);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoppingCart);
        }

        // GET: ShoppingCart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.ShoppingCart == null)
            {
                return NotFound();
            }

            var shoppingCart = await db.ShoppingCart.FindAsync(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            return View(shoppingCart);
        }

        // POST: ShoppingCart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Quantity,ProductId,TotalCost")] ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(shoppingCart);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartExists(shoppingCart.Id))
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
            return View(shoppingCart);
        }

        // GET: ShoppingCart/Delete/5
        public async Task<IActionResult> Delete(int? id, int pId)
        {
            if (id == null || db.ShoppingCart == null)
            {
                return NotFound();
            }

            var shoppingCart = await db.ShoppingCart
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (shoppingCart == null)
            {
                return NotFound();
            }
			var temp = db.Product.FirstOrDefault(s => s.Id == pId);
            ViewBag.my = temp.Id;
            return RedirectToAction("DeleteConfirmed", new { id = id, pId = pId });
		}

        // POST: ShoppingCart/Delete/5
       // [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int pId)
        {
            if (db.ShoppingCart == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ShoppingCart'  is null.");
            }
            var shoppingCart = await db.ShoppingCart.FindAsync(id);
            if (shoppingCart != null)
            {
                
                db.ShoppingCart.Remove(shoppingCart);
				var incrementProduct = db.Product.FirstOrDefault(s => s.Id == pId);
				incrementProduct.Stock+=shoppingCart.Quantity;
				db.Entry(incrementProduct).State = EntityState.Modified;
			}
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartExists(int id)
        {
          return (db.ShoppingCart?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
