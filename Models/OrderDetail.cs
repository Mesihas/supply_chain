using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFDC.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        //One Order has many OrderDetails
        public int OrderId { get; set; }
        public int SubCategory { get; set; }
        public int Category { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public string ObsOrder { get; set; }

        //public int QuantityOrdered { get; set; }

        //public int QuantityDispatched { get; set; }
        //public string ObsDispatched { get; set; }
        //public int QuantityDelivered { get; set; }
        //public string ObsDelivered { get; set; }





    }
}
