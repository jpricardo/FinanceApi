namespace FinanceApi.Models.DTOs.Auth
{
    public class UserRegisterDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}