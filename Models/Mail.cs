using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFDC.Models
{
    public class Mail
    {
        public int MailId { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public DateTime Fecha { get; set; }
        public string Subject { get; set; }
        public string MailTo { get; set; }
        public string MailToName { get; set; }
        public string CC { get; set; }
        public string Body { get; set; }
        public bool Visto { get; set; }
        public int Estado { get; set; }
                 
    }
}
