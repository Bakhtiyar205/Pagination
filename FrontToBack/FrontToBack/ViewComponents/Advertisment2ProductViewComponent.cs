using FrontToBack.Data;
using FrontToBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.ViewComponents
{
    public class Advertisment2ProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public Advertisment2ProductViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Advertisment2Product> advertisment2Products = await _context.Advertisment2Products
                .OrderBy(m=>m.Id)
                .Take(2)
                .ToListAsync();

            return await Task.FromResult(View(advertisment2Products));
        }
    }
}
