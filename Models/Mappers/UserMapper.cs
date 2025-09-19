using FinanceApi.Models.DTOs.Auth;
using FinanceApi.Models.Entities;
using FinanceApi.Services;

namespace FinanceApi.Models.Mappers
{
    public class UserMapper
    {
        public static User MapToUser(UserRegisterDTO dto)
        {
            DateTime now = DateTime.Now;

            var (hash, salt) = PasswordService.HashPassword(dto.Password);

            return new User
            {
                Username = dto.Username,
                PasswordHash = hash,
                PasswordSalt = salt,

                CreatedAt = now,
            };
        }
    }
}
