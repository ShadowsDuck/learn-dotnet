using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_dotnet.Models
{
    public class Comment
    {
        // Table column
        public int Id { get; set; } // Primary key
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        // Relation
        public int? StockId { get; set; } // Foreign key
        public Stock? Stock { get; set; } // Navigation property
    }
}