using CardBlock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardBlock.Entities
{
    public interface ICardBlockRepository
    {
        Task<CardBlockResponse> BlockCard(CardBlockRequest request);
        bool isDuplicateID(string idString);
        string EncData(string value);
        CardBlockRequest GetCardBlockRequest(Request request);
    }
}
