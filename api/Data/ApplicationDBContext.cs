using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace learn_dotnet.Data
{
    // dotnet ef migrations add init, dotnet ef database update
    public class ApplicationDBContext : DbContext // DbContext = ตัวแทนของ Database ใน EF Core
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stocks { get; set; } // DbSet<Stock> = แทนตาราง Stocks
        public DbSet<Comment> Comments { get; set; } // DbSet<Comment> = แทนตาราง Comments

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ตั้งความสัมพันธ์ Stock 1 - * Comment, ถ้าลบ Stock → ลบ Comment อัตโนมัติ
            modelBuilder.Entity<Comment>()
                .HasOne(comment => comment.Stock)           // Comment มี Stock
                .WithMany(stock => stock.Comments)          // Stock มีหลาย Comment
                .HasForeignKey(comment => comment.StockId)  // FK คือ StockId
                .OnDelete(DeleteBehavior.Cascade);          // Cascade Delete
        }
    }
}