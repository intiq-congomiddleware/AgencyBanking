using AgencyBanking.Entities;
using CashDeposit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashDeposit.Entities
{
    public interface ICashDepositRepository
    {
        Task<FundsTransferResponse> CashDeposit(CashDepositRequest request);
        CashDepositRequest GetCashDepositRequest(Request r);
        bool isDuplicateID(string idString);
        string EncData(string value);
        Models.Response GetFTResponse(FundsTransferResponse r);
    }
}
