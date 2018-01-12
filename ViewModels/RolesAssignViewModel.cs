using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EFDC.ViewModels
{
    public class RolesAssignViewModel
    {
        public IList<string> Roles { get; set; }
        public string User { get; set; }
        public IList<string> RolesAvalilable { get; set; }
        public string Action { get; set; }
        public string rol { get; set; }
        public int ProfileId { get; set; }
        public string Message { get; set; }
    }
}
