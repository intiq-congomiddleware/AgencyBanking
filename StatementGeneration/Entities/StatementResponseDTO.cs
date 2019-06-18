using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatementGeneration.Entities
{
    public class StatementResponseDTO
    {
        public string RUN_USERID { get; set; }
        public string ACCT_NO { get; set; }
        public string TXN_DAT { get; set; }
        public string VAL_DT { get; set; }
        public string TXN_BRN { get; set; }
        public string COD_USER_ID { get; set; }
        public string TXN_DESC { get; set; }
        public string REF_NO { get; set; }
        public string DR_AMT { get; set; }
        public string CR_AMT { get; set; }
        public string RUNNING_BAL { get; set; }
        public string DAT_POST { get; set; }
        public string TRANSACTION_REFERENCE { get; set; }
        public string TRAN_MNEMONIC { get; set; }
        public string TRANS_SEQNO { get; set; }
        public string RUNDATE { get; set; }
        public string STARTDATE { get; set; }
        public string ENDDATE { get; set; }
        public string TOTALDR { get; set; }
        public string TOTALCR { get; set; }
        public string DRCOUNT { get; set; }
        public string CRCOUNT { get; set; }
        public string CURRENCY { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string OPENINGBALANCE { get; set; }
        public string CLOSINGBALANCE { get; set; }
        public string ADDRESS { get; set; }
        public string BRANCH_CODE { get; set; }
        public string ACCOUNT_CLASS { get; set; }
        public string AC_STAT_NO_DR { get; set; }
        public string AC_STAT_NO_CR { get; set; }
        public string AC_STAT_BLOCK { get; set; }
        public string AC_STAT_STOP_PAY { get; set; }
        public string AC_STAT_DORMANT { get; set; }
        public string AC_STAT_FROZEN { get; set; }
        public string AC_STAT_DE_POST { get; set; }
        public string ACY_AVL_BAL { get; set; }
    }
}
