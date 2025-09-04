using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace learn_dotnet.Models
{
    public class Stock
    {
        // Table column
        public int Id { get; set; } // Primary key
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDividend { get; set; }
        public string Industry { get; set; } = string.Empty; // string.Empty = "" (string ว่าง) 
        // ใส่ไว้เพื่อ ให้ property เริ่มต้นเป็น string ว่างแทน null → ลดปัญหา null check
        public long MarketCap { get; set; }

        // Relation (one-to-many)
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}