using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundsTransfer.Models
{
    public class Request
    {
        public string debitAccount { get; set; }
        public string creditAccount { get; set; }
        public decimal amount { get; set; }      
        public string narration { get; set; }
        public string requestId { get; set; }
    }
}
