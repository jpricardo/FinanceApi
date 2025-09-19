using FinanceApi.Data.Enum;

namespace FinanceApi.Models
{
    public class GetExpenseDTO
    {
        public required long Id { get; set; }
        public required string Description { get; set; }
        public required ExpenseTypeEnum ExpenseType { get; set; }
        public required decimal Amount { get; set; }
        public required CurrencyEnum Currency { get; set; }
        public required DateOnly Date { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }
    }
}
