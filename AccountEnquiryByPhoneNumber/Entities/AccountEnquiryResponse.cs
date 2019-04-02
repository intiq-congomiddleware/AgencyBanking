using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountEnquiry.Entities
{
    public class AccountEnquiryResponse
    {
        public string cod_cc_brn { get; set; }
        public string cod_acct_no { get; set; }
        public string cod_acct_title { get; set; }
        [JsonIgnore]
        public string account_type { get; set; }
        [JsonIgnore]
        public string nam_ccy_short { get; set; }
        public string dat_acct_open { get; set; }
        [JsonIgnore]
        public string cod_cust { get; set; }
        [JsonIgnore]
        public string ac_stat_no_dr { get; set; }
        [JsonIgnore]
        public string ac_stat_no_cr { get; set; }
        [JsonIgnore]
        public string ac_stat_block { get; set; }
        [JsonIgnore]
        public string ac_stat_stop_pay { get; set; }
        public string ac_stat_dormant { get; set; }
        [JsonIgnore]
        public string ac_stat_frozen { get; set; }
        [JsonIgnore]
        public string cod_prod { get; set; }
        [JsonIgnore]
        public string ac_stat_de_post { get; set; }
        [JsonIgnore]
        public string accountdesc { get; set; }
        [JsonIgnore]
        public string branch { get; set; }
        public string bal_available { get; set; }
        [JsonIgnore]
        public string date_of_birth { get; set; }
        [JsonIgnore]
        public string customer_category { get; set; }
        [JsonIgnore]
        public string responsecode { get; set; }
        [JsonIgnore]
        public string responsemessage { get; set; }
    }
}
