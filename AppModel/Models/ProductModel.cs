using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel.Models
{
    //Model class of Product
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required(ErrorMessage ="Short Description is required")]
        [StringLength(100,MinimumLength = 10)]
        public string SDes { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 20)]
        public string LDes { get; set; }

        public string SImg { get; set; }
 
        public string LImg { get; set; }
        public int User_id { get; set; }
    }
}
