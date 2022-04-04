using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Models
{
    public class Slider:BaseEntity
    {
        [Required]
        public string Image { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ClassName { get; set; }


    }
}
