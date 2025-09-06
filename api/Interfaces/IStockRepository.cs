using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_dotnet.Dtos.Stock;
using learn_dotnet.Models;

namespace learn_dotnet.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();

        Task<Stock?> GetByIdAsync(int id); // FirstOrDefault can be Null ก็เลยใช้ Stock?

        Task<Stock> CreateAsync(Stock createStockDto);

        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockDto);

        Task<Stock?> DeleteAsync(int id);
    }
}