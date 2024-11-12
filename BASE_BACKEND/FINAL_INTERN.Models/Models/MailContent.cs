using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_INTERN.Models.Models
{
    public class MailContent
    {
        // nguoi nhan  email
        public string To { get; set; }

        // tieu de email
        public string Subject { get; set; }

        //noi dung email
        public string Body { get; set; }
    }
}
