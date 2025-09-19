using FinanceApi.Models;

namespace FinanceApi.Mappers
{
    public static class IncomeMapper
    {
        public static GetIncomeDTO MapToGetIncomeDTO(Income income)
        {
            return new GetIncomeDTO
            {
                Id = income.Id,
                Description = income.Description,
                IncomeType = income.IncomeType,
                Amount = income.Amount,
                Currency = income.Currency,
                Date = income.Date,
                CreatedAt = income.CreatedAt,
                UpdatedAt = income.UpdatedAt
            };
        }
        public static Income MapToIncome(CreateIncomeDTO dto)
        {
            DateTime now = DateTime.Now;

            return new Income
            {
                Description = dto.Description,
                Amount = dto.Amount,
                Currency = dto.Currency,
                Date = dto.Date,

                CreatedAt = now,
                UpdatedAt = now
            };
        }
        public static Income MapToIncome(UpdateIncomeDTO dto, Income income)
        {
            DateTime now = DateTime.Now;

            income.Description = dto.Description;
            income.IncomeType = dto.IncomeType;
            income.Amount = dto.Amount;
            income.Currency = dto.Currency;
            income.Date = dto.Date;

            income.UpdatedAt = now;

            return income;
        }
    }
}