using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionStatus.Entities
{
    public class StatusRequest
    {
        public string requestId { get; set; }
    }

    public class _StatusRequest
    {
        public string transactionRef { get; set; }
    }
}
