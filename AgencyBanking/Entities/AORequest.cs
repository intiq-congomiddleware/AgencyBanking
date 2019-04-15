using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyBanking.Entities
{
    public class AORequest
    {
        public string customerType { get; set; }
        public string customerName { get; set; }
        public string shortName { get; set; }
        public string customerCategory { get; set; }
        public string customerPrefix { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public string minor { get; set; }
        public string sex { get; set; }
        public string dAddress1 { get; set; }
        public string dAddress2 { get; set; }
        public string dAddress3 { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }       
        public string amountsCcy { get; set; }       
        public string branchCode { get; set; }
        public string country { get; set; }
        public string nationality { get; set; }
        public string language { get; set; }   
        public string accountClass { get; set; }      
        public string requestId { get; set; }

    }
}
