using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashWithdrawal.Models
{
    public class Request
    {
        public string debitAccount { get; set; }
        public decimal amount { get; set; }      
        public string narration { get; set; }
        public decimal chargeRate { get; set; }
        public string requestId { get; set; }
    }
}
