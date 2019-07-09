using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyBanking.Entities
{
    public class AOResponse
    {      
        public string customerNumber { get; set; }
        public string accountNumber { get; set; }
        public string customerName { get; set; }
        public string branchCode { get; set; }
        public string message { get; set; }
    }
}
