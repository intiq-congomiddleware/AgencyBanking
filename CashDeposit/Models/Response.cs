using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashDeposit.Models
{
    public class Response
    {
        public string status { get; set; }
        public string message { get; set; }
        public string actualTrnAmt { get; set; }
        public string rate { get; set; }
        public string trnRefNo { get; set; }
    }
}
