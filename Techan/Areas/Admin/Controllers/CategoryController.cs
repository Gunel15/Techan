using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Techan.DataAccessLayer;
using Techan.Models;
using Techan.ViewModels.Categories;

namespace Techan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController(TechanDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var datas = await _context.Categories.Select(x => new CategoryGetVM
            {
                Name = x.Name,
                Id = x.Id,
            }).ToListAsync();
            return View(datas);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            if(!ModelState.IsValid)
            return View(vm);
            await _context.Categories.AddAsync(new Category
            {
                Name = vm.Name
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue || id.Value < 1)
                return BadRequest();
            var category = await _context.Categories.Where(x => x.Id == id).Select(x => new CategoryUpdateVM
            {
                Name = x.Name
            }).FirstOrDefaultAsync();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,CategoryUpdateVM vm)
        {
            if (!id.HasValue || id.Value < 1)
                return BadRequest();
            if (!ModelState.IsValid) return View(vm);
            var category = await _context.Categories.FindAsync(id);
            if (category is null)
                return NotFound();
            category.Name = vm.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue || id.Value < 1)
                return BadRequest();
            int result = await _context.Categories.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (result == 0)
                return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
