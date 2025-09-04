using learn_dotnet.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// มันบอก EF Core ว่า “เวลาจะสร้าง ApplicationDBContext ให้ใช้ SQL Server และเชื่อมต่อด้วย connection string ชื่อ DefaultConnection”
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers(); // ลงทะเบียน service สำหรับ controller

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers(); // บอก framework ให้ใช้ routes จาก controllers

app.Run();

