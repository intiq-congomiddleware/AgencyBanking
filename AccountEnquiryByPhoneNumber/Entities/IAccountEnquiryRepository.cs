using AccountEnquiry.Entities;
using AgencyBanking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountEnquiry.Entities
{
    public interface IAccountEnquiryRepository
    {
        Task<Tuple<List<Models.Response>, Response>> GetPhoneEnquiryByAccountNumber(PhoneEnquiryRequest request);
    }
}
