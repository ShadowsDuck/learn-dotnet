using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_dotnet.Data;
using learn_dotnet.Interfaces;
using learn_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace learn_dotnet.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stocks.ToListAsync();
        }
    }
}