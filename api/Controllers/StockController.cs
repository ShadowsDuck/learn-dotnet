using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_dotnet.Data;
using learn_dotnet.Dtos.Stock;
using learn_dotnet.Interfaces;
using learn_dotnet.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace learn_dotnet.Controllers
{
    [Route("api/stock")] // base URL ของ controller นี้คือ /api/stock
    [ApiController] // บอกว่า controller นี้คือ API controller (ไม่ใช่ MVC view controller)
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;
        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _stockRepo.GetAllAsync();
            var stockDto = stocks.Select(s => s.ToStockDto()); // map แต่ละ Stock → StockDto
            return Ok(stockDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto()); // map เป็น StockDto
        }

        // [HttpPost] → บอกว่า method นี้จะถูกเรียกตอน client ส่ง POST request มาที่ /api/stocks
        // [FromBody] → ASP.NET Core จะ map JSON body ที่ client ส่งเข้ามา → ใส่ค่าเข้าไปใน createStockDto (ซึ่งคือ CreateStockRequestDto)
        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto createStockDto)
        {
            var stockModel = createStockDto.ToStockFromCreateDto(); // เราแปลง DTO ที่รับเข้ามา → เป็น Stock model (entity ของ DB)
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel.ToStockDto());
            // nameof(GetStockById), new { id = stockModel.Id } ใช้กำหนด ตำแหน่ง (Location) ของ resource ที่เพิ่งสร้าง
            // stockModel.ToStockDto() คือ payload (body) ที่ส่งกลับไปให้ user
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockDto)
        {
            var stockModel = await _stockRepo.UpdateAsync(id, updateStockDto);

            if (stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stockModel = await _stockRepo.DeleteAsync(id);

            if (stockModel == null)
            {
                return NotFound();
            }

            return NoContent(); // http status code 204 (No Content)
        }
    }
}