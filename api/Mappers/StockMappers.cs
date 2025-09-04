using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_dotnet.Dtos.Stock;
using learn_dotnet.Models;

namespace learn_dotnet.Mappers
{
    public static class StockMappers // ฟังก์ชันหรือ method ที่ทำหน้าที่ แปลงระหว่าง Entity ↔ DTO
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDividend = stockModel.LastDividend,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
            };
        }
    }
}

