using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using System.Security.Claims;

namespace AgriEnergyConnect.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            // Access the claims of the currently authenticated user
            var farmerIdClaim = User.FindFirstValue("FarmerID"); // or your custom claim type like "FarmerID"

            // If FarmerID is stored as a string in claims, and FarmerID in database is an integer
            if (int.TryParse(farmerIdClaim, out int farmerId))
            {
                // Filter the products to only include those owned by the logged-in farmer
                var applicationDbContext = _context.Products.Include(p => p.Farmer)
                                                            .Where(p => p.FarmerID == farmerId);

                return View(await applicationDbContext.ToListAsync());
            }

            // If FarmerID cannot be parsed, return an empty list or handle as appropriate
            return View(Enumerable.Empty<Product>());
        }

        public async Task<IActionResult> ViewProducts(DateTime? startDate, DateTime? endDate, int? farmerID, string category)
        {
            ViewBag.UserRole = "Employee";

            var productsQuery = _context.Products.Include(p => p.Farmer).AsQueryable();

            if (startDate.HasValue)
                productsQuery = productsQuery.Where(p => p.ProductionDate >= startDate.Value);

            if (endDate.HasValue)
                productsQuery = productsQuery.Where(p => p.ProductionDate <= endDate.Value);

            if (farmerID.HasValue)
                productsQuery = productsQuery.Where(p => p.Farmer.FarmerID == farmerID.Value);

            if (!string.IsNullOrEmpty(category))
                productsQuery = productsQuery.Where(p => p.Category == category);
            ViewBag.Categories = _context.Products
    .Select(p => p.Category)
    .Distinct()
    .OrderBy(c => c)
    .ToList();

            var products = await productsQuery.ToListAsync();


            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Farmer)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        //public IActionResult Create()
        //{
        //    ViewData["FarmerID"] = new SelectList(_context.Farmers, "FarmerID", "FarmerID");
        //    return View();
        //}


    public IActionResult Create()
        {
            var farmerIdClaim = User.FindFirstValue("FarmerID");

            if (int.TryParse(farmerIdClaim, out int farmerId))
            {
                var product = new Product
                {
                    FarmerID = farmerId // 👈 set logged-in FarmerID here
                };

                return View(product);
            }

            // Optional: redirect or show error if not a farmer
            return Unauthorized();
        }


    // POST: Product/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("ProductID,FarmerID,Name,Category,ProductionDate,Description,Price")] Product product)
    //{
    //    ViewBag.UserRole = "Employee";
    //    _context.Add(product);
    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //    ViewData["FarmerID"] = new SelectList(_context.Farmers, "FarmerID", "FarmerID", product.FarmerID);
    //    return View(product);
    //}

    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,Name,Category,ProductionDate,Description,Price")] Product product)
        {
            var farmerIdClaim = User.FindFirstValue("FarmerID");

            if (int.TryParse(farmerIdClaim, out int farmerId))
            {
                product.FarmerID = farmerId;

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return Unauthorized(); // if not a valid farmer
        }


        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["FarmerID"] = new SelectList(_context.Farmers, "FarmerID", "FarmerID", product.FarmerID);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,FarmerID,Name,Category,ProductionDate,Description,Price")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
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
            ViewData["FarmerID"] = new SelectList(_context.Farmers, "FarmerID", "FarmerID", product.FarmerID);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Farmer)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
