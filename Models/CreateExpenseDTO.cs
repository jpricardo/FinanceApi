using FinanceApi.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace FinanceApi.Models
{
    public class CreateExpenseDTO
    {
        [Required]
        public required string Description { get; set; }
        public required ExpenseTypeEnum ExpenseType { get; set; }
        public required decimal Amount { get; set; }
        public required CurrencyEnum Currency { get; set; }
        public required DateOnly Date { get; set; }
    }
}
