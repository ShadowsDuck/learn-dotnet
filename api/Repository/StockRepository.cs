using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_dotnet.Data;
using learn_dotnet.Dtos.Stock;
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

        public async Task<Stock> CreateAsync(Stock newStock)
        {
            await _context.Stocks.AddAsync(newStock);
            await _context.SaveChangesAsync();
            return newStock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stocks.Include(comment => comment.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(comment => comment.Comments)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(x => x.Id == id); // AnyAsync จะเช็คว่ามี id ไหมถ้ามีจะ return true ถ้าไม่มีจะ return false
        }

        public async Task<Stock?> UpdateAsync(int id, Stock updateStock)
        {
            var existingStock = await _context.Stocks.FindAsync(id);

            if (existingStock == null)
            {
                return null;
            }

            existingStock.Symbol = updateStock.Symbol;
            existingStock.CompanyName = updateStock.CompanyName;
            existingStock.Purchase = updateStock.Purchase;
            existingStock.LastDividend = updateStock.LastDividend;
            existingStock.Industry = updateStock.Industry;
            existingStock.MarketCap = updateStock.MarketCap;
            await _context.SaveChangesAsync();

            return existingStock;
        }
    }
}