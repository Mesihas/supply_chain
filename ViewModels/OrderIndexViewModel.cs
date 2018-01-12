using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDC.Models;

namespace EFDC.ViewModels
{
    public class OrderIndexViewModel
    {
        public int Total { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Branch> Branches { get; set; }
        public IEnumerable<Order> Orders { get; set; }

    }
}
