namespace FinanceApi.Models
{
    public class UpdateExpenseDTO
    {
        public long Id { get; set; }
        public required string Description { get; set; }
    }
}
