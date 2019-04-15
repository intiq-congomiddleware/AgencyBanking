using Channels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOpening.Entities
{
    public interface IAccountOpeningRepository
    {
        Task<AccountOpeningResponse> OpenAccount(AccountOpeningRequest request);
    }
}
