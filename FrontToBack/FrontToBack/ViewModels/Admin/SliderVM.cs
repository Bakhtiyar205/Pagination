using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.ViewModels.Admin
{
    public class SliderVM
    {
        public int Id { get; set; }

        public List<IFormFile> Photos { get; set; }
        public List<decimal> Prices { get; set; }
        public List<string> SubTitle { get; set; }
    }
}
