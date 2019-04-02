using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BalanceEnquiry.Entities
{
    public class BalanceEnquiryResponse
    {
        public string cust_name { get; set; }
        public string currency { get; set; }
        public string acct_type { get; set; }
        public string cod_prod { get; set; }
        public string cod_acct_no { get; set; }
        public string uncleard_bal { get; set; }
        public string bal_available { get; set; }
        public string cod_cc_brn { get; set; }
    }
}
