using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOpening.Entities
{
    public class AccountOpeningRequest
    {
        public string MAINTENANCE_SEQ_NO { get; set; }
        public string CUSTOMER_NO { get; set; }
        public string CUSTOMER_TYPE { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string SHORT_NAME { get; set; }
        public string CUSTOMER_CATEGORY { get; set; }
        public string EXPOSURE_CATEGORY { get; set; }
        public string CUSTOMER_PREFIX { get; set; }
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string DATE_OF_BIRTH { get; set; }
        public string LEGAL_GUARDIAN { get; set; }
        public string MINOR { get; set; }
        public string SEX { get; set; }
        public string NATIONAL_ID { get; set; }
        public string D_ADDRESS1 { get; set; }
        public string D_ADDRESS2 { get; set; }
        public string D_ADDRESS3 { get; set; }
        public string D_ADDRESS4 { get; set; }
        public string TELEPHONE { get; set; }
        public string FAX { get; set; }
        public string E_MAIL { get; set; }
        public string P_ADDRESS1 { get; set; }
        public string P_ADDRESS3 { get; set; }
        public string P_ADDRESS2 { get; set; }
        public string INCORP_DATE { get; set; }
        public string CAPITAL { get; set; }
        public string NETWORTH { get; set; }
        public string BUSINESS_DESCRIPTION { get; set; }
        public string AMOUNTS_CCY { get; set; }
        public string PASSPORT_NO { get; set; }
        public string PPT_ISS_DATE { get; set; }
        public string PPT_EXP_DATE { get; set; }
        public string BRANCH_CODE { get; set; }
        public string COUNTRY { get; set; }
        public string USER_ID { get; set; }
        public string NATIONALITY { get; set; }
        public string LANGUAGE { get; set; }
        public string UNIQUE_ID_NAME { get; set; }
        public string UNIQUE_ID_VALUE { get; set; }
        public string ACCOUNT_CLASS { get; set; }

        public string DR_HO_LINE { get; set; }
        public string CR_HO_LINE { get; set; }
        public string CR_CB_LINE { get; set; }
        public string DR_CB_LINE { get; set; }
        public string DR_GL { get; set; }
        public string CR_GL { get; set; }

        public bool STATUS { get; set; }
        public string ERROR_MESSAGE { get; set; }
        public string BATCH_ID { get; set; }
    }
}
