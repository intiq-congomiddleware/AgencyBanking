using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundsTransfer.Models
{
    public class Request
    {
        public string debitAccount { get; set; }
        public string creditAccount { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string typeofId { get; set; }
        public string idNumber { get; set; }
        public string placeofIssue { get; set; }
        public DateTime idExpirydate { get; set; }
        public string beneficiaryName { get; set; }
        public string beneficiaryAccount { get; set; }
        public string beneficiaryAddress { get; set; }
        public string paymentPurpose { get; set; }
        public string beneficiaryBank { get; set; }
        public string beneficiaryBanksortcode { get; set; }
        public string beneficiaryBankswiftcode { get; set; }
        public string intermediaryBankname { get; set; }
        public string intermediaryBankaddress { get; set; }
        public string residentPermitno { get; set; }
        public DateTime issueDate { get; set; }
        public DateTime expiryDate { get; set; }
        public string requestId { get; set; }
        public string userName { get; set; }
    }
}
