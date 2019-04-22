using AgencyBanking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionStatus.Entities
{
    public interface ITransactionStatusRepository
    {
        Task<FundsTransferResponse> ValidateTransactionByRef(_StatusRequest _request);
        Task<LogFundsTransfer> getTransactionRef(StatusRequest request);
        string EncData(string value);
    }
}
