using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BalanceEnquiry.Models
{
    public class Response
    {
        public string customerName { get; set; }
        public string currency { get; set; }
        public string accountType { get; set; }
        public string codProd { get; set; }
        public string codAccountNumber { get; set; }
        public decimal uncleardBalance { get; set; }
        public decimal availableBalance { get; set; }
        public string codCcBrn { get; set; }
    }
}
