using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFDC.Models
{
    public class Order
    {

        // Every Model must have OID that is the className followed by Id
        // the OID will become PK in the Database
        public int OrderId { get; set; }
        public int OrderStateId { get; set; }
        public int CategoryId { get; set; }
        public int BranchId { get; set; } ///check 1

        // One Order has many OrderDetails
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public DateTime Date { get; set; }
        public string UserCreateId {get; set;}

        // User that input the Order
        //public int UserCargaId { get; set; }
        //// User that Dispatches the Order
        //public int UserDispatchId { get; set; }
        //// User that Receives the Order
        //public int UserReceiveId { get; set; }
     
        //public Boolean Despatched { get; set; }
        //public DateTime DespatchedDate { get; set; }
        //public Boolean Received { get; set; }
        //public DateTime ReceivedDate { get; set; }

        //public String ObsDispatchHead { get; set; }
        //public String ObsReceived { get; set; }
        //public String ObsCargaSucursal { get; set; }
        //public int Claim { get; set; }
        //public int ViewClaim { get; set; }



    }
}
