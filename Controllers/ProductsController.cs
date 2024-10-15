using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Otello.Data;
using Otello.Models;
using SQLitePCL;

namespace Otello.Controllers
{
    [Route("/[controller]/[action]")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext db;
        public static int oldColor;
        public ProductsController(ApplicationDbContext context)
        {
            db = context;
        }
        static ViewModel brandList = null;
      
        public async Task<IActionResult> Apparel()
        {
            List<Product> products = new List<Product>();
            products = db.Product.ToList();
            List<ProductImages> productsImages = new List<ProductImages>();
            productsImages = db.ProductImages.ToList();

            ViewModel model = new ViewModel();
            model.ProductModel = db.Product.ToList();
            model.ProductImagesModel = db.ProductImages.ToList();

            return View(model);
                       
        }

        [HttpPost]
        [Route("/Products/Category/{type}")]
        public async Task<IActionResult> Category(string type, ViewModel model, int maxslider, int minslider)
        {
            System.Diagnostics.Debug.WriteLine("c1 -----------------------------------");
            System.Diagnostics.Debug.WriteLine(model.Checkboxes);
            System.Diagnostics.Debug.WriteLine(type);
            ViewBag.Type = type;
            System.Diagnostics.Debug.WriteLine(maxslider);
            List<Product> products = new List<Product>();
            List<ProductImages> productsImages = new List<ProductImages>();
            products = db.Product.ToList();
            ViewBag.FilterList = new List<string>();
            model.ProductReviewsModel = db.ProductReviews.ToList();
            ViewBag.chosenMax = maxslider;
            model.chosenMax = ViewBag.chosenMax;
            ViewBag.chosenMin = minslider;
            model.chosenMin = ViewBag.chosenMin;

            productsImages = db.ProductImages.ToList();
            model.ProductModel = db.Product.ToList();
            model.ProductImagesModel = db.ProductImages.ToList();
           
            List<string> brands = new List<string>();
            brands.Clear();
           
            model.Checkboxes = new List<CheckboxOptions>();
              
                foreach(var item in products) 
                {
                    if(item.Type == type && !brands.Contains(item.Brand))
                    {
                        brands.Add(item.Brand);

                        model.Checkboxes.Add(
                       new CheckboxOptions()
                           {
                               IsChecked = false,
                               Description = item.Brand,
                               Value = item.Brand
                           }
                       );
                    }
                   
                   
                }
            model.Checkboxes.Add(
                   new CheckboxOptions()
                   {
                       IsChecked = false,
                       Description = "Men's",
                       Value = "M"
                      
                   }
                   );
            model.Checkboxes.Add(
                  new CheckboxOptions()
                  {
                      IsChecked = false,
                      Description = "Women's",
                      Value = "W"

                  }
                  );
            model.Checkboxes.Add(
                  new CheckboxOptions()
                  {
                      IsChecked = false,
                      Description = "Unisex",
                      Value = "U"

                  }
                  );
            brandList = model;




            return RedirectToAction("Category", new { type = type});

        }
        [HttpGet]
        [Route("/Products/Category/{type}")]
        public async Task<IActionResult> Category(string type)
        {
            
            System.Diagnostics.Debug.WriteLine("c2 --------------------------------");
            ViewBag.Type = type;
            
            List<Product> products = new List<Product>();
            List<ProductImages> productsImages = new List<ProductImages>();
            products = db.Product.ToList();
            ViewBag.FilterList = new List<string>();

            List<string> brands = new List<string>();
            brands.Clear();
            productsImages = db.ProductImages.ToList();
            //System.Diagnostics.Debug.WriteLine("IN C2 THE BRAND LIST I: " + brandList.UniqueName.Count);
            if (brandList == null)  
            {
                
                ViewModel model = new ViewModel();
                ViewBag.chosenMax = model.MaxPrice;
                ViewBag.chosenMin = model.MinPrice;
                model.ProductModel = db.Product.ToList();
                model.ProductImagesModel = db.ProductImages.ToList();
                model.ProductReviewsModel = db.ProductReviews.ToList();
                model.Checkboxes = new List<CheckboxOptions>();
              
                foreach(var item in products)
                {
                    if(item.Type == type && !brands.Contains(item.Brand))
                    {
                        brands.Add(item.Brand);

                        model.Checkboxes.Add(
                       new CheckboxOptions()
                           {
                               IsChecked = false,
                               Description = item.Brand,
                               Value = item.Brand
                           }
                       );
                    }
                   
                }
                model.Checkboxes.Add(
                   new CheckboxOptions()
                   {
                       IsChecked = false,
                       Description = "Men's",
                       Value = "M"

                   }
                   );
                model.Checkboxes.Add(
                      new CheckboxOptions()
                      {
                          IsChecked = false,
                          Description = "Women's",
                          Value = "W"

                      }
                      );
                model.Checkboxes.Add(
                      new CheckboxOptions()
                      {
                          IsChecked = false,
                          Description = "Unisex",
                          Value = "U"

                      }
                      );
                return
                View(model);
            }

            ViewBag.chosenMax = brandList.chosenMax;
            ViewBag.chosenMin = brandList.chosenMin;
            ViewModel temp = new ViewModel();
            temp = brandList;
            brandList = null;


            return
                View(temp);

        }
        
        [Route("/Products/ProductDisplay/{id}+{colorId}+{sizeId}+{variationId}")]
		public async Task<IActionResult> ProductDisplay(int id, int colorId, int sizeId, int variationId)
		{
			var temp = db.ProductVariations.FirstOrDefault(s => s.productId == id && s.colorId ==  colorId);
			System.Diagnostics.Debug.WriteLine("test: " + colorId + oldColor);
			if (colorId != oldColor)
            {
				System.Diagnostics.Debug.WriteLine("yup");
				ViewBag.SizeId = temp.sizeId;
			}
            else
            {
				ViewBag.SizeId = sizeId;
			}

			ViewBag.ColorId = colorId;
            oldColor = colorId;
			ViewBag.Id = id;
            System.Diagnostics.Debug.WriteLine("1" + id +  colorId +  sizeId +  variationId);
			ViewBag.vId = variationId;

			ViewBag.ProductReviewsModel = db.ProductReviews.ToList();
			ViewBag.ProductSizesModel = db.ProductSizes.ToList();
			ViewBag.ProductColorsModel = db.ProductColors.ToList();
			ViewBag.Pe = db.ProductVariations.ToList();
			ViewBag.ProductImagesModel = db.ProductImages.ToList();
			ViewBag.ProductModel = db.Product.ToList();
			return
				View("ProductDisplay");
		}
        
		[HttpGet]
        [Route("/Products/ProductDisplay/{id}")]
        public async Task<IActionResult> ProductDisplay(int id)
        {

			//for view data
			//ViewData["productImages"] = db.ProductImages.ToList();
			//ViewData["product"] = db.Product.ToList();
			ViewBag.Pe = db.ProductVariations.ToList();
			ViewBag.Id = id;
            var temp = db.ProductVariations.FirstOrDefault(s => s.productId == id);
            ViewBag.vId = temp.Id;
            ViewBag.ColorId = temp.colorId;
            oldColor = temp.colorId;
            ViewBag.SizeId = temp.sizeId;
			ViewBag.ProductSizesModel = db.ProductSizes.ToList();
			ViewBag.ProductColorsModel = db.ProductColors.ToList();
			ViewBag.ProductReviewsModel = db.ProductReviews.ToList();
			ViewBag.ProductImagesModel = db.ProductImages.ToList();
            ViewBag.ProductModel = db.Product.ToList();
			
			return
                View();
        }
        public async Task<IActionResult> onSubmit(int id, int c2Id, int s2Id, int v2Id, bool dbOperationSuccess)
        {
			System.Diagnostics.Debug.WriteLine("2" + id + c2Id + s2Id + v2Id);
			int send = id;
            //for view data
            //ViewData["productImages"] = db.ProductImages.ToList();
            //ViewData["product"] = db.Product.ToList();
            ViewData["Alert"] = dbOperationSuccess;
            ViewBag.Id = id;
			ViewBag.Pe = db.ProductVariations.ToList();
			ViewBag.ProductColorsModel = db.ProductColors.ToList();

			ViewBag.ProductImagesModel = db.ProductImages.ToList();
            ViewBag.ProductModel = db.Product.ToList();

            return
                RedirectToAction("ProductDisplay", new { id=send, colorId=c2Id, sizeId=s2Id, variationId=v2Id });
        }

		[HttpPost]
        public async Task<IActionResult> Review([Bind("Id,ProductId,Name,Message,Rating")] ProductReviews review)
		{
			System.Diagnostics.Debug.WriteLine("id is  " + review.ProductId);
			bool dbOperationSuccess = false;
			
			db.ProductReviews.Add(review);
			
			//decrements the stock for the product item's table
			if (db.SaveChanges() > 0)
			{
				dbOperationSuccess = true;
			}
			return RedirectToAction("ProductDisplay", new {id=review.ProductId});

		}
        public async Task<IActionResult> UpdateReview([Bind("Id,ProductId,Name,Message,Rating")] ProductReviews review)
        {

            System.Diagnostics.Debug.WriteLine("testing: " + review.Id + " / " + review.Message);
            if (ModelState.IsValid)
            {
                try
                {
                    var reviewToUpdate =  db.ProductReviews.FirstOrDefault(s => s.Id == review.Id);
                    reviewToUpdate.Message = review.Message;
                    reviewToUpdate.Rating = review.Rating;
                    db.Entry(reviewToUpdate).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                        return NotFound();
                   
                }
            }
				return RedirectToAction("ProductDisplay", "Products", new { id = review.ProductId });
		}
		// GET: Products
		public async Task<IActionResult> Index()
        {
          
              return db.Product != null ? 
                          View(await db.Product.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Product'  is null.");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Product == null)
            {
                return NotFound();
            }

            var product = await db.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Rating,Stock,Price,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Product == null)
            {
                return NotFound();
            }

            var product = await db.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Rating,Stock,Price,CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(product);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Product == null)
            {
                return NotFound();
            }

            var product = await db.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Product == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
            }
            var product = await db.Product.FindAsync(id);
            if (product != null)
            {
                db.Product.Remove(product);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (db.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
