using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string SDes { get; set; }
        public string LDes { get; set; }
        public string ImageURL { get; set; }
    }
}
