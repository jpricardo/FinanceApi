using FinanceApi.Models;

namespace FinanceApi.Mappers
{
    public static class ExpenseMapper
    {
        public static GetExpenseDTO MapToGetExpenseDTO(Expense expense)
        {
            return new GetExpenseDTO
            {
                Id = expense.Id,
                Description = expense.Description,
                ExpenseType = expense.ExpenseType,
                Ammount = expense.Ammount,
                Currency = expense.Currency,
                Date = expense.Date,
                CreatedAt = expense.CreatedAt,
                UpdatedAt = expense.UpdatedAt
            };
        }
        public static Expense MapToExpense(UpdateExpenseDTO dto, Expense expense)
        {
            DateTime now = DateTime.Now;

            expense.Description = dto.Description;
            expense.ExpenseType = dto.ExpenseType;
            expense.Ammount = dto.Ammount;
            expense.Currency = dto.Currency;
            expense.Date = dto.Date;

            expense.UpdatedAt = now;

            return expense;
        }
        public static Expense MapToExpense(CreateExpenseDTO dto)
        {
            DateTime now = DateTime.Now;

            return new Expense
            {
                Description = dto.Description,
                ExpenseType = dto.ExpenseType,
                Ammount = dto.Ammount,
                Currency = dto.Currency,
                Date = dto.Date,

                CreatedAt = now,
                UpdatedAt = now
            };
        }



    }
}
