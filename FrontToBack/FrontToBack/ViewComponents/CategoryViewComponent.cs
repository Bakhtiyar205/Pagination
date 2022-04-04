using FrontToBack.Data;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public CategoryViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> categories = await _context.Categories.Include(m=>m.SubCategories).ToListAsync();
            List<CategoryAdvertisment> categoryAdvertisments = await _context.CategoryAdvertisments.ToListAsync();

            CategoryVM categoryVM = new CategoryVM()
            {
                Categories = categories,
                CategoryAdvertisments = categoryAdvertisments
            };


            return await Task.FromResult(View(categoryVM));
        }
    }
}
