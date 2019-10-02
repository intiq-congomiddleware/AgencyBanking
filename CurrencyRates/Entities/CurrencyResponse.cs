using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyRates.Entities
{
    public class CurrencyResponse
    {
        public string status { get; set; }
        public decimal EUR { get; set; }
        public decimal GBP { get; set; }
        public decimal NGN { get; set; }
        public decimal USD { get; set; }
    }
}
