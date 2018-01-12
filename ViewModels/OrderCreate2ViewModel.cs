using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDC.Models;
namespace EFDC.ViewModels
{
    public class OrderCreate2ViewModel
    {
        public string Category { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<SubCategory> Subcategories { get; set; }
        public int Total { get; set; }
        public int CategoryId { get; set; }

    }
}
