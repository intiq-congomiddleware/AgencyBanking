using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundsTransfer.Entities
{
    public class FundsTransferRequest
    {
        public string dract { get; set; }        
        public string cract { get; set; }
        public decimal trnamt { get; set; }
        public string trnrefno { get; set; }
        public string l_acs_ccy { get; set; }
        public string txnnarra { get; set; }
        public string product { get; set; }
        public string instr_code { get; set; }
        public string branch_code { get; set; }
        public string responseCode { get; set; }
        public string authorization { get; set; }
        public string user_name { get; set; }
        public string guid { get; set; }
        public int trans_type { get; set; }
        public bool is_own_account { get; set; }
    }
}
