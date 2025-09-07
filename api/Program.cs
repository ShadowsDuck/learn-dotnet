using learn_dotnet.Data;
using learn_dotnet.Interfaces;
using learn_dotnet.Repository;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// มันบอก EF Core ว่า “เวลาจะสร้าง ApplicationDBContext ให้ใช้ SQL Server และเชื่อมต่อด้วย connection string ชื่อ DefaultConnection”
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers(); // ลงทะเบียน service สำหรับ controller

// บอก DI Container ว่าเวลาเจอ IStockRepository → ให้ใช้ StockRepository
// ใช้ AddScoped = มีอายุการใช้งานต่อ 1 Request (สร้างใหม่ทุก request)
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers(); // บอก framework ให้ใช้ routes จาก controllers

app.Run();

