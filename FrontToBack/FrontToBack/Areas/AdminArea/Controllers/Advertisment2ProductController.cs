using FrontToBack.Data;
using FrontToBack.Models;
using FrontToBack.Utilities.Files;
using FrontToBack.Utilities.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class Advertisment2ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _enviroment;
        public Advertisment2ProductController(AppDbContext context, IWebHostEnvironment enviroment)
        {
            _context = context;
            _enviroment = enviroment;
        }
        public async Task<IActionResult> Index()
        {
            List<Advertisment2Product> advertisment2Products = await _context.Advertisment2Products
                .Where(m => !m.IsDeleted)
                .Take(2)
                .ToListAsync();
            return View(advertisment2Products);
        }

        public IActionResult Create()
        {
            return View();
        }



        //For Create Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, Advertisment2Product advertisment)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
            if (!advertisment.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Only Image Type is Acceptible");
                return View();
            }
            if (!advertisment.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Please minimize image size less than 200KB");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + advertisment.Photo.FileName;

            string path = Helper.GetFilePath(_enviroment.WebRootPath, "assets/img", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await advertisment.Photo.CopyToAsync(stream);
            }

            advertisment.Image = fileName;
            await _context.Advertisment2Products.AddAsync(advertisment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        

        //For Delete Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Advertisment2Product advertisment = await FindAdvertismentById(id);
            if (advertisment is null) return NotFound();

            string path = Helper.GetFilePath(_enviroment.WebRootPath, "assets/img", advertisment.Image);

            Helper.DeleteFile(path);

            _context.Advertisment2Products.Remove(advertisment);

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        //For Find Method for advertisment product
        public async Task<Advertisment2Product> FindAdvertismentById(int id)
        {
            return await _context.Advertisment2Products.FindAsync(id);
        }


        //For Update Method
        public async Task<IActionResult> Update(int id)
        {
            Advertisment2Product advertisment2Product = await _context.Advertisment2Products.FindAsync(id);

            if (advertisment2Product is null) NotFound();

            return View(advertisment2Product);
        }

        //For Update Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Advertisment2Product advertisment)
        {
            Advertisment2Product dbAdvertisment = await _context.Advertisment2Products.FindAsync(id);

            string path = Helper.GetFilePath(_enviroment.WebRootPath, "assets/img", dbAdvertisment.Image);

            Helper.DeleteFile(path);

            string discount = advertisment.Discount;
            string text = advertisment.Text;
            string subText = advertisment.Text;

            if(advertisment.Photo != null)
            {
                if (!advertisment.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Only image type is accebtible");
                    return View(advertisment);
                }

                if (!advertisment.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Please Upload File less than 200KB");
                    return View(advertisment);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + advertisment.Photo.FileName;

                string pathNew = Helper.GetFilePath(_enviroment.WebRootPath, "assets/img", fileName);

                using (FileStream stream = new FileStream(pathNew, FileMode.Create))
                {
                    await advertisment.Photo.CopyToAsync(stream);
                }
                dbAdvertisment.Image = fileName;
            }

            dbAdvertisment.Discount = discount;
            dbAdvertisment.Text = text;
            dbAdvertisment.SubText = subText;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //For Detail Method
        public async Task<IActionResult> Detail(int id)
        {
            Advertisment2Product advertisment = await _context.Advertisment2Products.FindAsync(id);
            return View(advertisment);
        }
    }
}
