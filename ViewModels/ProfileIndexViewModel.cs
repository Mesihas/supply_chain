using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDC.Models;

namespace EFDC.ViewModels
{
    public class ProfileIndexViewModel
    {
        public int ProfileId { get; set; }
        public string UserId { get; set; }       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Total { get; set; }
        public bool Activo { get; set; }
        public IEnumerable<Branch> Branches { get; set; }       
    }
}
