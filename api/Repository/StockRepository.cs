using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_dotnet.Data;
using learn_dotnet.Dtos.Stock;
using learn_dotnet.Helpers;
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

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        // ประกาศ method แบบ async ที่ return List ของ Stock 
        // โดยรับ parameter query (ใช้เป็น filter เงื่อนไขการค้นหา)
        {
            var stocks = _context.Stocks
                .Include(comment => comment.Comments) // ดึงข้อมูล Comments ของแต่ละ Stock มาด้วย (Eager Loading)
                .AsQueryable(); // แปลงให้เป็น IQueryable เพื่อให้ต่อเงื่อนไข Where ได้แบบ dynamic

            if (!string.IsNullOrWhiteSpace(query.Symbol)) // ถ้า Symbol ใน query ไม่ว่าง
            {
                stocks = stocks.Where(stock => stock.Symbol.Contains(query.Symbol));
                // กรองข้อมูล stocks ให้เหลือเฉพาะ record ที่ Symbol มีข้อความที่ตรงกับ query.Symbol
            }

            if (!string.IsNullOrWhiteSpace(query.CompanyName)) // ถ้า CompanyName ใน query ไม่ว่าง
            {
                stocks = stocks.Where(stock => stock.CompanyName.Contains(query.CompanyName));
                // กรองข้อมูล stocks ให้เหลือเฉพาะ record ที่ CompanyName มีข้อความที่ตรงกับ query.CompanyName
            }

            return await stocks.ToListAsync();
            // Execute query จริง ๆ บนฐานข้อมูล และดึงผลลัพธ์ออกมาเป็น List<Stock>
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