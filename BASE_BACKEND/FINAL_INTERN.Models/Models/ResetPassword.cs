using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_INTERN.Models.Models
{
    public partial class ResetPassword
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
