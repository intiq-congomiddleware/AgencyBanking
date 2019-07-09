using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyBanking.Entities
{
    public class LogAccountBlock
    {
        public string accountNumber { get; set; }
        public string requestId { get; set; }
        public string reason { get; set; }
        public string userName { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string request { get; set; }
        public string response { get; set; }
    }
}
