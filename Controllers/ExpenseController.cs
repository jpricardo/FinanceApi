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

        private bool ExpenseExists(long id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }

        [HttpGet("", Name = "GetAllExpenses")]
        public async Task<ActionResult<IEnumerable<Expense>>> GetAll()
        {
            return await _context.Expenses.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetExpense")]
        public async Task<ActionResult<Expense>> Get(long id)
        {
            Expense? item = await _context.Expenses.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost("", Name = "CreateExpense")]
        public async Task<ActionResult<Expense>> Post(CreateExpenseDTO createExpenseDTO)
        {
            Expense expense = new Expense
            {
                Description = createExpenseDTO.Description
            };

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

            Expense expense = new Expense
            {
                Id = updateExpenseDTO.Id,
                Description = updateExpenseDTO.Description
            };

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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
