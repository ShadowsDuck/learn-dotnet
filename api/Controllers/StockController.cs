using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_dotnet.Data;
using learn_dotnet.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace learn_dotnet.Controllers
{
    [Route("api/stock")] // base URL ของ controller นี้คือ /api/stock
    [ApiController] // บอกว่า controller นี้คือ API controller (ไม่ใช่ MVC view controller)
    public class StockController : ControllerBase
    {
        /* ASP.NET Core จะ inject ApplicationDBContext เข้ามาอัตโนมัติ 
        (เพราะเราเคยทำ builder.Services.AddDbContext<ApplicationDBContext>() ไว้) */
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context; // _context คือ object สำหรับ query ข้อมูลจาก Database
        }

        [HttpGet]
        public IActionResult GetAllStocks()
        {
            var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto()); // map แต่ละ Stock → StockDto

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetStockById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto()); // map เป็น StockDto
        }
    }
}