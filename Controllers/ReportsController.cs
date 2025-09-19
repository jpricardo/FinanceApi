using FinanceApi.Data;
using FinanceApi.Models.Entities;
using FinanceApi.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace FinanceApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReportsController(ExpenseContext expenseContext, IncomeContext incomeContext) : ControllerBase
    {
        private readonly ExpenseContext _expenseContext = expenseContext;
        private readonly IncomeContext _incomeContext = incomeContext;

        [HttpGet("expenses/csv")]
        public async Task<IActionResult> ExportExpensesToCsv(
            [FromQuery] ExpenseTypeEnum? type,
            [FromQuery] DateOnly? startDate,
            [FromQuery] DateOnly? endDate,
            [FromQuery] decimal? minAmount,
            [FromQuery] decimal? maxAmount)
        {
            IQueryable<Expense> query = _expenseContext.Expenses;

            if (type.HasValue)
            {
                query = query.Where(e => e.ExpenseType == type.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(e => e.Date >= startDate);
            }
            if (endDate.HasValue)
            {
                query = query.Where(e => e.Date <= endDate);
            }

            if (minAmount.HasValue)
            {
                query = query.Where(e => e.Amount >= minAmount);
            }
            if (maxAmount.HasValue)
            {
                query = query.Where(e => e.Amount <= maxAmount);
            }

            var expenses = await query.ToListAsync();

            var csv = new StringBuilder();
            csv.AppendLine("Id,Description,ExpenseType,Amount,Currency,Date,CreatedAt,UpdatedAt");

            foreach (Expense e in expenses)
            {
                csv.AppendLine(string.Join(",",
                    e.Id,
                    EscapeCsv(e.Description),
                    e.ExpenseType,
                    e.Amount.ToString(CultureInfo.InvariantCulture),
                    e.Currency,
                    e.Date.ToString("yyyy-MM-dd"),
                    e.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    e.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                ));
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", "expenses.csv");
        }


        [HttpGet("incomes/csv")]
        public async Task<IActionResult> ExportIncomesToCsv(
            [FromQuery] IncomeTypeEnum? type,
            [FromQuery] DateOnly? startDate,
            [FromQuery] DateOnly? endDate,
            [FromQuery] decimal? minAmount,
            [FromQuery] decimal? maxAmount)
        {
            IQueryable<Income> query = _incomeContext.Incomes;

            if (type.HasValue)
            {
                query = query.Where(e => e.IncomeType == type.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(e => e.Date >= startDate);
            }
            if (endDate.HasValue)
            {
                query = query.Where(e => e.Date <= endDate);
            }

            if (minAmount.HasValue)
            {
                query = query.Where(e => e.Amount >= minAmount);
            }
            if (maxAmount.HasValue)
            {
                query = query.Where(e => e.Amount <= maxAmount);
            }

            var incomes = await query.ToListAsync();

            var csv = new StringBuilder();
            csv.AppendLine("Id,Description,IncomeType,Amount,Currency,Date,CreatedAt,UpdatedAt");

            foreach (var i in incomes)
            {
                csv.AppendLine(string.Join(",",
                    i.Id,
                    EscapeCsv(i.Description),
                    i.IncomeType,
                    i.Amount.ToString(CultureInfo.InvariantCulture),
                    i.Currency,
                    i.Date.ToString("yyyy-MM-dd"),
                    i.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    i.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                ));
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", "incomes.csv");
        }

        private static string EscapeCsv(string value)
        {
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
                return $"\"{value.Replace("\"", "\"\"")}\"";
            return value;
        }
    }
}