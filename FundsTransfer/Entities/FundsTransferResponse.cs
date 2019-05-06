using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundsTransfer.Entities
{
    public class FundsTransferResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public string actualtrnamt { get; set; }
        public string rate { get; set; }
        public string trnrefno { get; set; }
        public string id { get; set; }
    }
}
