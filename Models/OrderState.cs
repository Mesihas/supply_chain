using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFDC.Models
{
    public class OrderState
    {
        public int OrderStateId { get; set; }
        public string Name { get; set; }
        // One OrderState has many Orders
        public virtual ICollection<Order> Orders { get; set; }

    }
}
