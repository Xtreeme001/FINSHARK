using AutoMapper;
using FINSHARK.Data;
using FINSHARK.Dtos.Stock;
using FINSHARK.Helpers;
using FINSHARK.Interfaces;
using FINSHARK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FINSHARK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStockRepository _stockRepo;

        public StocksController(ApplicationDbContext context, IMapper mapper, IStockRepository stockRepo)
        {
            _context = context;
            _mapper = mapper;
            _stockRepo = stockRepo;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockRepo.GetAllAsync(query);
            var stockModelDto = _mapper.Map<List<StockDto>>(stockModel).ToList();
            return Ok(stockModelDto);
        }
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            var stockDto = _mapper.Map<StockDto>(stock);

            return Ok(stockDto);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateStockDto createStockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = _mapper.Map<Stock>(createStockDto);
            await _stockRepo.CreateAsync(stockModel);
            var stockDto = _mapper.Map<StockDto>(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockDto);
        }
        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockRepo.UpdateAsync(id, updateStockDto);
            if (stockModel == null)
            {
                return NotFound();
            }
            var stockDto = _mapper.Map<StockDto>(stockModel);
            return Ok(stockDto);

        }
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockRepo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent();

        }
    }
}

