using Channels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashDeposit.Entities
{
    public interface ICashDepositRepository
    {
        Task<FundsTransferResponse> CashDeposit(CashDepositRequest request);
    }
}
