using FinanceApi.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApi.Models
{
    public class Income
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string Description { get; set; } = "";
        public IncomeTypeEnum IncomeType { get; set; }
        public decimal Amount { get; set; }
        public CurrencyEnum Currency { get; set; }
        public DateOnly Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}