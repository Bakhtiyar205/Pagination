using FrontToBack.Data;
using FrontToBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var category = await _context.Categories.Where(m => !m.IsDeleted).ToListAsync();
            return View(category);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var category = _context.Categories.Where(m => m.Id == id).FirstOrDefault();
            return Json(new
            {
                categoryName = category.Name,
                action = "Detail",
                Id = id
            });
        }

        //For Edit
        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _context.Categories.AsNoTracking().Where(m => !m.IsDeleted && m.Id == id).FirstOrDefaultAsync();

            if (category == null) return BadRequest();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Category category)
        {
            if (!ModelState.IsValid) return View();
            if (id != category.Id) BadRequest();

            try
            {
                Category dbCatgegory = await _context.Categories.AsNoTracking().Where(m => !m.IsDeleted && m.Id == id).FirstOrDefaultAsync();

                if (dbCatgegory.Name.ToLower().Trim() == category.Name.ToLower().Trim()) RedirectToAction(nameof(Index));

                bool isExist = _context.Categories.Where(m => !m.IsDeleted).Any(m => m.Name.ToLower().Trim() == category.Name.ToLower().Trim());

                if (isExist)
                {
                    ModelState.AddModelError("Name", "Bu Category Artiq Movcuddur");
                    return View();
                }


                //dbCatgegory == category but use without AsNoTracking, it is good for one changes but more than one it is recommended use AsNoTracking
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }


        //For Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _context.Categories.Where(m => !m.IsDeleted && m.Id == id).FirstOrDefaultAsync();

            if (category == null) NotFound();

            //_context.Categories.Remove(category);

            category.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExist = _context.Categories.Any(m => m.Name.ToLower().Trim() == category.Name.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu Ad Movcuddur");
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
