using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyBanking.Entities
{
    public class LogAccountOpening
    {
        public string requestId { get; set; }
        public string customerNo { get; set; }
        public string accountNo { get; set; }
        public string customerName { get; set; }
        public string branchCode { get; set; }
        public string customerType { get; set; }
        public string request { get; set; }
        public string response { get; set; }
    }
}
