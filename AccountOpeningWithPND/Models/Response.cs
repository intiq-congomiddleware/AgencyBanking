using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOpening.Models
{
    public class Response
    {
        public string customerNumber { get; set; }
        public string accountNumber { get; set; }
        public string customerName { get; set; }
        public string branchCode { get; set; }
        public string message { get; set; }
    }
}
