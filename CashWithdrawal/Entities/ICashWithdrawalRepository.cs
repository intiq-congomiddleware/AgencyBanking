using Channels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashWithdrawal.Entities
{
    public interface ICashWithdrawalRepository
    {
        Task<FundsTransferResponse> CashWithdrawal(CashWithdrawalRequest request);
    }
}
