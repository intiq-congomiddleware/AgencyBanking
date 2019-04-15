using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyBanking.Entities
{
    public class FTResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public string actualtrnamt { get; set; }
        public string rate { get; set; }
        public string trnrefno { get; set; }
        public string id { get; set; }
    }
}
