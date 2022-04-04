
using FrontToBack.Data;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) BadRequest();

            Product dbProduct = await GetProductById(id);

            if (dbProduct == null) return BadRequest();

            List<BasketVM> basket = GetBasket();

            UpdateBasket(basket, dbProduct);

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return Json(new { 
              data=dbProduct
            });
        }

        private List<BasketVM> GetBasket()
        {
            List<BasketVM> basket;
            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket;
        }

        private void UpdateBasket(List<BasketVM> basket, Product product)
        {
            var existProduct = basket.Find(m => m.Id == product.Id);

            if (existProduct == null)
            {
                basket.Add(new BasketVM
                {
                    Id = product.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }
        }

        private async Task<Product> GetProductById(int? id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IActionResult> Basket()
        {

            List<BasketVM> basket;
            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }
            List<BasketDetailVM> basketDetailsItem = new List<BasketDetailVM>();

            foreach (BasketVM basketItem in basket)
            {
                
                Product product = await _context.Products.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == basketItem.Id);

                BasketDetailVM basketDetail = new BasketDetailVM
                {
                    Id = basketItem.Id,
                    ProductName = product.Name,
                    ProductImage = product.Images.Where(m => m.IsMain).FirstOrDefault().Image,
                    Count = basketItem.Count,
                    Price = product.Price * basketItem.Count
                };

                basketDetailsItem.Add(basketDetail);
            }
            return View(basketDetailsItem);
            //return Json(JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]));
        }

    }
}
