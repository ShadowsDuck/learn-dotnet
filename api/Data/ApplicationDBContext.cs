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

        public DbSet<Stock> Stock { get; set; } // DbSet<Stock> = แทนตาราง Stocks
        public DbSet<Comment> Comment { get; set; } // DbSet<Comment> = แทนตาราง Comments
    }
}