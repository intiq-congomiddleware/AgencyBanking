using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyBanking.Entities
{
    public class FTRequest
    {
        public string debitAccount { get; set; }
        public string creditAccount { get; set; }
        public decimal amount { get; set; }      
        public string narration { get; set; }
        public string requestId { get; set; }
        public string userName { get; set; }
        public string branchCode { get; set; }
        public string product { get; set; }
    }
}
