using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyRates.Entities
{
    public interface ICurrencyRepository
    {
        Task<CurrencyResponse> GetRates(CurrencyRequest request);
        bool isDuplicateID(string idString);
        string EncData(string value);
    }
}
