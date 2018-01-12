using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDC.Models;

namespace EFDC.ViewModels
{
    public class CategoryDetailsViewModel
    {
        public int Total { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
