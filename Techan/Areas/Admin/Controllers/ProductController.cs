using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Techan.DataAccessLayer;
using Techan.Extension;
using Techan.Models;
using Techan.ViewModels.Brands;
using Techan.ViewModels.Products;

namespace Techan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(TechanDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            //var prods=await _context.Products
            //    .Include(x=>x.Brand)
            //    .Include(x=>x.Category)
            //    .ToListAsync();
            var prods = await _context.Products.Select(x => new ProductListVM
            {
                BrandName = x.Brand.Name,
                Name = x.Name,
                CostPrice = x.CostPrice,
                SellPrice = x.SellPrice,
                Discount = x.Discount,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                CategoryName = x.Category.Name,
                IsDeleted=x.IsDeleted
            }).ToListAsync();
            return View(prods);
        }


        public async Task<IActionResult> Create()
        {
            //var brands = await _context.Brands.Select(x => new BrandGetVM
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //}).ToListAsync();
            //ViewBag.Brands = brands;
            //ViewBag.Categories = await _context.Categories.ToListAsync();
            await _fillViewBags();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            if (vm.ImageFile != null)
            {
                if (!vm.ImageFile.IsValidType("image"))
                    ModelState.AddModelError("ImageFile", "File must be image");
                if (!vm.ImageFile.IsValidSize(200))
                    ModelState.AddModelError("ImageFile", "File size must be equal or less than 200kb");
            }
            if (vm.ImageFiles != null)
            {
                if (!vm.ImageFiles.All(x => x.IsValidType("image")))
                    ModelState.AddModelError("ImageFiles", "Files must be image");
                if (!vm.ImageFiles.All(x => x.IsValidSize(150)))
                    ModelState.AddModelError("ImageFiles", "File size must be equal or less than 150kb");
            }
            if (!ModelState.IsValid)
            {
                await _fillViewBags();
                return View(vm);
            }

            if (!await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Category does not exit");
                await _fillViewBags();
                return View(vm);
            }

            if (!await _context.Brands.AnyAsync(x => x.Id == vm.BrandId))
            {
                ModelState.AddModelError("BrandId", "Brand does not exit");
                await _fillViewBags();
                return View(vm);
            }
            Product product = new Product
            {
                Name = vm.Name,
                Description = vm.Description,
                CostPrice = vm.CostPrice,
                SellPrice = vm.SellPrice,
                BrandId = vm.BrandId,
                CategoryId = vm.CategoryId,
                Discount = vm.Discount,
                ImageUrl = await vm.ImageFile!.UploadAsync(Path.Combine("wwwroot", "imgs", "products"))
            };
            #region Version1
           // product.Images ICollection tipinden olmalidir
            product.Images = [];
            foreach (var image in vm.ImageFiles ?? [])
            {
                product.Images.Add(new ProductImage
                {
                    ImageUrl = await image.UploadAsync(Path.Combine("wwwroot", "imgs", "products"))
                });
            }
            #endregion
            #region Version2
            //product.Images = vm.ImageFiles?.Select(x => new ProductImage
            //{
            //    ImageUrl = x.UploadAsync(Path.Combine("wwwroot", "imgs", "products")).Result
            //});
            #endregion

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        async Task _fillViewBags()
        {
            ViewBag.Brands = await _context.Brands.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
        }

        public async Task<IActionResult> SoftDelete(int? id)
        {
            if (!id.HasValue || id.Value < 1)
                return BadRequest();
            var data = await _context.Products.FindAsync(id);
            if (data is null)
                return NotFound();
            data.IsDeleted = !data.IsDeleted;
            await _context.SaveChangesAsync();
            TempData["IsDeleted"] = true;
            return RedirectToAction(nameof(Index));
        }
    }
}
