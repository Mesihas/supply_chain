using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDC.Models;


namespace EFDC.ViewModels
{
    public class BranchIndexViewModel
    {
        public int Total { get; set; }
        public IEnumerable<Branch> Branches { get; set; }
    }
}
