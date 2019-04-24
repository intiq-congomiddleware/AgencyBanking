using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashDeposit.Entities
{
    public class FundsTransferResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        [JsonIgnore]
        public string actualtrnamt { get; set; }
        [JsonIgnore]
        public string rate { get; set; }
        //[JsonIgnore]
        public string trnrefno { get; set; }
        [JsonIgnore]
        public string id { get; set; }
    }
}
