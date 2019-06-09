using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatementGeneration.Entities
{
    public class StatementResponse
    {
        public DateTime transactionDate { get; set; }
        public string transactionDesc { get; set; }
        public DateTime valueDate { get; set; }
        public decimal debit { get; set; }
        public decimal credit { get; set; }
        public decimal balance { get; set; }
    }
}
