using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_dotnet.Models;

namespace learn_dotnet.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
    }
}