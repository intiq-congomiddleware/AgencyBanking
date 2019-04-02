using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyBanking.Entities
{
    public class Constant
    {
        public const string TIMESTAMP_FORMAT_1 = "yyyy-MM-dd HH:mm:ss:fff";
        public const string TIMESTAMP_FORMAT_2 = "yyyyMMddHHmmss";
        public const string CONTENTTYPE = "application/json";

        //General Responses
        public const string TIMEOUT_MSG = "Transaction Timeout: Confirm Transaction Status before Retrying.";
        public const string UKNOWN_MSG = "Uknown Response: Confirm Transaction Status before Retrying.";

        //General Statuses
        public const string TIMEOUT_STATUS = "Timeout";
        public const string FAILED_STATUS = "Failed";
        public const string UKNOWN_STATUS = "Unkown";
        public const string UNPROCESSABLE_REQUEST = "Request could not be processed.";

        //Account Opening Responses
        public const string ACC_CREATION_SUCCESSFUL = "Account Created Successfully";
        public const string ACC_CREATION_FAILED = "Account Creation Failed";

        //Funds Transfer Responses
        public const string PST_FAILED_STATUS = "Posting Failed";

    }
}
