using AccountOpening.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOpening.Entities
{
    public interface IAccountOpeningRepository
    {
        Task<Response> OpenAccount(AccountOpeningRequest request);
        AccountOpeningRequest GetAccountOpeningRequest(Request r);
    }
}
