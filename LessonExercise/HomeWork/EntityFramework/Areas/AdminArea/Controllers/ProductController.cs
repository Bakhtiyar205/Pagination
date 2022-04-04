using EntityFramework.Data;
using EntityFramework.Models;
using EntityFramework.Utilities.Pagination;
using EntityFramework.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 10)
        {
            var products = await _context.Products
                .Include(m => m.Images)
                .Include(m => m.Category)
                .Skip((page-1)*take)
                .Take(take)
                .AsNoTracking()
                .OrderByDescending(m => m.Id)
                .ToListAsync();

            var productsVM = GetMapDatas(products);

            int count = await GetPageCount(take);

            Paginate<ProductListVM> result = new Paginate<ProductListVM>(productsVM, page, count);

            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _context.Categories.Where(m => !m.IsDeleted).ToListAsync();
            ViewBag.categories = new SelectList(categories, "Id", "Name");
            return View();
        }


        //For Paginate
        private async Task<int> GetPageCount(int take)
        {
            var count = await _context.Products.CountAsync();

            return (int)Math.Ceiling((decimal)count / take);
        }


        //For equality between VM and Model
        private List<ProductListVM> GetMapDatas(List<Product> products)
        {
            List<ProductListVM> productList = new List<ProductListVM>();
            foreach (var product in products)
            {
                ProductListVM newProduct = new ProductListVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    Image = product.Images.Where(m=>m.IsMain).FirstOrDefault()?.Image,
                    CategoryName = product.Category.Name,
                    Count = product.Count,
                    Price = product.Price
                };

                productList.Add(newProduct);
            }

            return productList;
        }
    }

}
