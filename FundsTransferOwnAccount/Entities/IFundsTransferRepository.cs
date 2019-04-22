using FundsTransfer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundsTransfer.Entities
{
    public interface IFundsTransferRepository
    {
        Task<FundsTransferResponse> FundsTransfer(FundsTransferRequest request);
        FundsTransferRequest GetFundsTransferRequest(Request r);
        string EncData(string value);
    }
}
