using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatementGeneration.Entities
{
    public class StatementRequest
    {
        public string accountNumber { get; set; }
        public int noOfRecords { get; set; }
        [JsonIgnore]
        public string userId { get; set; }
        [JsonIgnore]
        public DateTime startDate { get; set; }
        [JsonIgnore]
        public DateTime endDate { get; set; }
    }
}
