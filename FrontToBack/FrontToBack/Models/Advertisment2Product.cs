using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Models
{
    public class Advertisment2Product:BaseEntity
    {
        public string Image { get; set; }
        public string Discount { get; set; }
        public string Text { get; set; }
        public string SubText { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
    }
}
