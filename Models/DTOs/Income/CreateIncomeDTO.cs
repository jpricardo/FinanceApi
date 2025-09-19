using FinanceApi.Models.Enums;

namespace FinanceApi.Models.DTOs.Income
{
    public class CreateIncomeDTO
    {
        public required string Description { get; set; }
        public required IncomeTypeEnum IncomeType { get; set; }
        public required decimal Amount { get; set; }
        public required CurrencyEnum Currency { get; set; }
        public required DateOnly Date { get; set; }
    }
}