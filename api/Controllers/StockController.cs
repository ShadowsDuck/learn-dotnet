using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_dotnet.Data;
using learn_dotnet.Dtos.Stock;
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

        // [HttpPost] → บอกว่า method นี้จะถูกเรียกตอน client ส่ง POST request มาที่ /api/stocks
        // [FromBody] → ASP.NET Core จะ map JSON body ที่ client ส่งเข้ามา → ใส่ค่าเข้าไปใน createStockDto (ซึ่งคือ CreateStockRequestDto)
        [HttpPost]
        public IActionResult CreateStock([FromBody] CreateStockRequestDto createStockDto)
        {
            var stockModel = createStockDto.ToStockFromCreateDto(); // เราแปลง DTO ที่รับเข้ามา → เป็น Stock model (entity ของ DB)
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel.ToStockDto());
            // nameof(GetStockById), new { id = stockModel.Id } ใช้กำหนด ตำแหน่ง (Location) ของ resource ที่เพิ่งสร้าง
            // stockModel.ToStockDto() คือ payload (body) ที่ส่งกลับไปให้ user
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockDto)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if (stockModel == null)
            {
                return NotFound();
            }

            stockModel.Symbol = updateStockDto.Symbol;
            stockModel.CompanyName = updateStockDto.CompanyName;
            stockModel.Purchase = updateStockDto.Purchase;
            stockModel.LastDividend = updateStockDto.LastDividend;
            stockModel.Industry = updateStockDto.Industry;
            stockModel.MarketCap = updateStockDto.MarketCap;

            _context.SaveChanges();

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStock([FromRoute] int id)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if (stockModel == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stockModel);
            _context.SaveChanges();

            return NoContent(); // http status code 204 (No Content)
        }
    }
}