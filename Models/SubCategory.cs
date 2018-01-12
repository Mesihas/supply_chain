using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFDC.Models
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }
        // One Category has many SubCategories
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string Color { get; set; }

    }
}
