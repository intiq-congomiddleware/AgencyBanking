using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOpening.Models
{
    public class Request
    {
        //public string MAINTENANCE_SEQ_NO { get; set; }
        public string customerType { get; set; }
        public string customerName { get; set; }
        public string shortName { get; set; }
        public string customerCategory { get; set; }
        //public string EXPOSURE_CATEGORY { get; set; }
        public string customerPrefix { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        //public string LEGAL_GUARDIAN { get; set; }
        public string minor { get; set; }
        public string sex { get; set; }
        //public string NATIONAL_ID { get; set; }
        public string dAddress1 { get; set; }
        public string dAddress2 { get; set; }
        public string dAddress3 { get; set; }
        //public string D_ADDRESS4 { get; set; }
        public string telephone { get; set; }
        //public string FAX { get; set; }
        public string email { get; set; }
        //public string P_ADDRESS1 { get; set; }
        //public string P_ADDRESS3 { get; set; }
        //public string P_ADDRESS2 { get; set; }
        //public string INCORP_DATE { get; set; }
        //public string CAPITAL { get; set; }
        //public string NETWORTH { get; set; }
        //public string BUSINESS_DESCRIPTION { get; set; }
        public string amountsCcy { get; set; }
        //public string PASSPORT_NO { get; set; }
        //public string PPT_ISS_DATE { get; set; }
        //public string PPT_EXP_DATE { get; set; }
        public string branchCode { get; set; }
        public string country { get; set; }
        //public string USER_ID { get; set; }
        public string nationality { get; set; }
        public string language { get; set; }
        //public string UNIQUE_ID_NAME { get; set; }
        //public string UNIQUE_ID_VALUE { get; set; }
        public string accountClass { get; set; }

        //public string DR_HO_LINE { get; set; }
        //public string CR_HO_LINE { get; set; }
        //public string CR_CB_LINE { get; set; }
        //public string DR_CB_LINE { get; set; }
        //public string DR_GL { get; set; }
        //public string CR_GL { get; set; }

        //public bool STATUS { get; set; }
        //public string ERROR_MESSAGE { get; set; }
        //public string BATCH_ID { get; set; }
        public string requestId { get; set; }

    }
}
