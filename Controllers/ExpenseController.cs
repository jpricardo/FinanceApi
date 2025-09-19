using FinanceApi.Mappers;
using FinanceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController(ExpenseContext context) : ControllerBase
    {
        private readonly ExpenseContext _context = context;

        [HttpGet("", Name = "GetAllExpenses")]
        public async Task<ActionResult<IEnumerable<GetExpenseDTO>>> GetAll()
        {
            List<GetExpenseDTO> expenses = await _context.Expenses.Select(e => ExpenseMapper.MapToGetExpenseDTO(e)).ToListAsync();
            return expenses;
        }

        [HttpGet("{id}", Name = "GetExpense")]
        public async Task<ActionResult<GetExpenseDTO>> Get(long id)
        {
            Expense? item = await _context.Expenses.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return ExpenseMapper.MapToGetExpenseDTO(item);
        }

        [HttpPost("", Name = "CreateExpense")]
        public async Task<ActionResult<Expense>> Post(CreateExpenseDTO createExpenseDTO)
        {
            Expense expense = ExpenseMapper.MapToExpense(createExpenseDTO);

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { Id = expense.Id }, expense);
        }

        [HttpPut("{id}", Name = "UpdateExpense")]
        public async Task<IActionResult> Put(long id, UpdateExpenseDTO updateExpenseDTO)
        {
            if (id != updateExpenseDTO.Id)
            {
                return BadRequest();
            }

            Expense? existingExpense = await _context.Expenses.FindAsync(updateExpenseDTO.Id);

            if (existingExpense == null)
            {
                return NotFound();
            }

            ExpenseMapper.MapToExpense(updateExpenseDTO, existingExpense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteExpense")]
        public async Task<IActionResult> Delete(long id)
        {
            Expense? expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
