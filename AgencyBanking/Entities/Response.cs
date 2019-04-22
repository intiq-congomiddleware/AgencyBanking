using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyBanking.Entities
{
    public enum currency
    {
        CDF,
        USD
    }
    public class Response
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class Response2
    {
        public bool status { get; set; }
        public Dictionary<string, string> message { get; set; }
    }
}
