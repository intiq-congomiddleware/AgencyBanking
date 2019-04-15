using AccountEnquiry.Entities;
using Channels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountEnquiry.Entities
{
    public interface IAccountEnquiryRepository
    {
        Task<Tuple<List<AccountEnquiryResponse>, Response>> GetCustomerEnquiryByAccountNumber(CustomerEnquiryRequest request);
    }
}
