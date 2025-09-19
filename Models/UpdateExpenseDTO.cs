using FinanceApi.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace FinanceApi.Models
{
    public class UpdateExpenseDTO
    {
        [Required]
        public required long Id { get; set; }
        [Required]
        public required string Description { get; set; }
        public required ExpenseTypeEnum ExpenseType { get; set; }
        public required decimal Ammount { get; set; }
        public required CurrencyEnum Currency { get; set; }
        public required DateOnly Date { get; set; }
    }
}
