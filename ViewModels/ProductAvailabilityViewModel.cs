using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDC.Models;

namespace EFDC.ViewModels
{
    public class ProductAvailabilityViewModel
    {
        public IEnumerable<Branch> Branches { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int BranchId { get; set; }
        public int CategoryId { get; set; }
    }
}
