using AgencyBanking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatementGeneration.Entities
{
    public interface IStatementRepository
    {
        Task<Tuple<List<StatementResponse>, Response>> GenerateStatement(StatementRequest request);
        string EncData(string value);
    }
}
