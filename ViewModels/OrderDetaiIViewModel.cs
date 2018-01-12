using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDC.Models;

namespace EFDC.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Total { get; set; }
        public int OrderId { set; get; }
        public string BranchName { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public string State { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<SubCategory> Subcategories { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        //???
        public string ProductDetails { get; set; }

    }
}
