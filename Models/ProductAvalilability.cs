using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFDC.Models
{
    public class ProductAvalilability
    {
        public int ProductAvalilabilityId { get; set; }
        public int BranchId { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public bool Available { get; set; }
        public string ProductName { get; set; }

    }
}
 