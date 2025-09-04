using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_dotnet.Dtos.Stock
{
    public class StockDto // ตัวกลางสำหรับส่งข้อมูลออกจาก API
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDividend { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
    }
}