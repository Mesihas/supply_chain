using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDC.Models;

namespace EFDC.ViewModels
{
    public class SubCategoryIndexViewModel
    {
        public int Total { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
        //public int CategoryId { get; set; }
        //public string SubCategoryName { get; set; }

    }
}
