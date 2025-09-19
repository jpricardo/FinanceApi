using FinanceApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinanceApi.Models.DTOs.Expense
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
