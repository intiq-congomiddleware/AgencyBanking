using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardBlock.Models
{
    public class Request
    {
        public string accountNumber { get; set; }
        public string requestId { get; set; }
        public string reason { get; set; }
        public string userName { get; set; }
    }
}
