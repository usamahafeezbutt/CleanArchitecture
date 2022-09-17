

namespace CleanArchitecture.Application.Services.Account.Dto
{
    public class ChangePasswordDto
    {
        public string Password { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
