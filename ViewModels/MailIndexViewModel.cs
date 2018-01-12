using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDC.Models;

namespace EFDC.ViewModels
{
    public class MailIndexViewModel
    {
        public IEnumerable<Mail> InBoxToMe { get; set; }
        public IEnumerable<Mail> InBoxCC { get; set; }
        public int Total { get; set; }
        public int TotalCC { get; set; }
        public bool Act { get; set; }
    }
}
