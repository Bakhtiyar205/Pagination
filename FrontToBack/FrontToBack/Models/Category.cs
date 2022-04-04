using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Models
{
    public class Category:BaseEntity
    {
        public string Icon { get; set; }
        [Required(ErrorMessage = "Burani Doldurun")]
        [StringLength(20,ErrorMessage ="Uzunluq 20 Karakterden Artiq Ola Bilmez")]
        public string Name { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
