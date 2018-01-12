using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDC.Models;
namespace EFDC.ViewModels
{
    public class OrderCreateViewModel
    {    
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Branch> Branches { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int BranchId { get; set; }
        public int CategoryId { get; set; }
    }
}
