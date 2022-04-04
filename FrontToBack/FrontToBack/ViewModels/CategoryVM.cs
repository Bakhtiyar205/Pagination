using FrontToBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.ViewModels
{
    public class CategoryVM
    {
        public List<Category> Categories { get; set; }
        public List<CategoryAdvertisment> CategoryAdvertisments { get; set; }
    }
}
