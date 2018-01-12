using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFDC.Models
{
    public class Product
    {
        public int ProductId { get; set; } // OID
        // one Category has many Products
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Codigo { get; set; }
  //      [NotMapped]
     //   public int Quantity { get; set; }

    }
}
