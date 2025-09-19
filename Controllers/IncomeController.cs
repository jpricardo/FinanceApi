using FinanceApi.Data;
using FinanceApi.Models.DTOs.Income;
using FinanceApi.Models.Entities;
using FinanceApi.Models.Enums;
using FinanceApi.Models.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeController(IncomeContext context) : ControllerBase
    {
        private readonly IncomeContext _context = context;

        [HttpGet("", Name = "GetAllIncomes")]
        public async Task<ActionResult<IEnumerable<GetIncomeDTO>>> GetAll(
            [FromQuery] IncomeTypeEnum? type,
            [FromQuery] DateOnly? startDate,
            [FromQuery] DateOnly? endDate,
            [FromQuery] decimal? minAmount,
            [FromQuery] decimal? maxAmount)
        {
            IQueryable<Income> query = _context.Incomes;

            // Filter by type
            if (type.HasValue)
            {
                query = query.Where(e => e.IncomeType == type.Value);
            }

            // Filter by date
            if (startDate.HasValue)
            {
                query = query.Where(e => e.Date >= startDate);
            }
            if (endDate.HasValue)
            {
                query = query.Where(e => e.Date <= endDate);
            }

            // Filter by amount
            if (minAmount.HasValue)
            {
                query = query.Where(e => e.Amount >= minAmount);
            }
            if (maxAmount.HasValue)
            {
                query = query.Where(e => e.Amount <= maxAmount);
            }

            List<GetIncomeDTO> incomes = await query.Select(e => IncomeMapper.MapToGetIncomeDTO(e)).ToListAsync();

            return incomes;
        }

        [HttpGet("{id}", Name = "GetIncome")]
        public async Task<ActionResult<GetIncomeDTO>> Get(long id)
        {
            Income? item = await _context.Incomes.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return IncomeMapper.MapToGetIncomeDTO(item);
        }

        [HttpPost("", Name = "CreateIncome")]
        public async Task<ActionResult<GetIncomeDTO>> Post(CreateIncomeDTO createIncomeDTO)
        {
            Income income = IncomeMapper.MapToIncome(createIncomeDTO);

            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { Id = income.Id }, income);
        }

        [HttpPut("{id}", Name = "UpdateIncome")]
        public async Task<IActionResult> Put(long id, UpdateIncomeDTO updateIncomeDTO)
        {
            if (id != updateIncomeDTO.Id)
            {
                return BadRequest();
            }

            Income? existingIncome = await _context.Incomes.FindAsync(updateIncomeDTO.Id);

            if (existingIncome == null)
            {
                return NotFound();
            }

            IncomeMapper.MapToIncome(updateIncomeDTO, existingIncome);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteIncome")]
        public async Task<IActionResult> Delete(long id)
        {
            Income? income = await _context.Incomes.FindAsync(id);

            if (income == null)
            {
                return NotFound();
            }

            _context.Incomes.Remove(income);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}