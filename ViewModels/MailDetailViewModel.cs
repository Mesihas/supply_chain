using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFDC.ViewModels
{
    public class MailDetailViewModel
    {
        public int MailId { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Fecha { get; set; }
        public bool mailcc { get; set; }

    }
}
