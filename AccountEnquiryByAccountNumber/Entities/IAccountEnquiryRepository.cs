﻿using AccountEnquiry.Entities;
using AgencyBanking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountEnquiry.Entities
{
    public interface IAccountEnquiryRepository
    {
        Task<Tuple<Models.Response, Response>> GetAccountEnquiryByAccountNumber(AccountEnquiryRequest request);
    }
}
