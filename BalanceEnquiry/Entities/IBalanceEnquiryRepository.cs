using Channels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BalanceEnquiry.Entities
{
    public interface IBalanceEnquiryRepository
    {
        Task<Tuple<BEResponse, Response>> GetBalanceByAccountNumber(BalanceEnquiryRequest request);
    }
}
