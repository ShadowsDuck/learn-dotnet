using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace learn_dotnet.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol must be at most 10 characters long")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(50, ErrorMessage = "CompanyName must be at most 50 characters long")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000000000, ErrorMessage = "Purchase must be between 1 and 1,000,000,000")]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 100, ErrorMessage = "LastDividend must be between 0.001 and 100")]
        public decimal LastDividend { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Industry must be at most 50 characters long")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 5000000000, ErrorMessage = "MarketCap must be between 1 and 5,000,000,000")]
        public long MarketCap { get; set; }
    }
}