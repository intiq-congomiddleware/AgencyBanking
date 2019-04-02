using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountEnquiry.Models
{
    public class Response
    {
        public string codCcBrn { get; set; }
        public string codAccountNumber { get; set; }
        public string codAccountTitle { get; set; }
        public string dateAccountOpen { get; set; }
        public string accountStatusDormant { get; set; }
        public decimal availableBalance { get; set; }
    }
}
