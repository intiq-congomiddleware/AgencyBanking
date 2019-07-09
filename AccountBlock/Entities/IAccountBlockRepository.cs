using AccountBlock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountBlock.Entities
{
    public interface IAccountBlockRepository
    {
        Task<AccountBlockResponse> BlockAccount(AccountBlockRequest request);
        bool isDuplicateID(string idString);
        string EncData(string value);
        AccountBlockRequest GetAccountBlockRequest(Request request);
    }
}
