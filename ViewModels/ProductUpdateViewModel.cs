using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDC.Models;

namespace EFDC.ViewModels
{
    public class ProductUpdateViewModel
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string Codigo { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public int ProductId { get; set; }
    }
}
