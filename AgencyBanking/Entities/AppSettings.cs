using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyBanking.Entities
{
    public class AppSettings
    {
        public string FlexSchema { get; set; }
        public string TMESchema { get; set; }
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Expires { get; set; }
        public string FlexConnection { get; set; }
        public string TMEConnection { get; set; }
        public string[] loggerModeOn { get; set; }
        public string baseURL { get; set; }
        public string beURL { get; set; }
        public string aeURL { get; set; }
        public string ceURL { get; set; }
        public string peURL { get; set; }
        public string GLAccount { get; set; }
        public string GLChrgAccount { get; set; }
    }
}

