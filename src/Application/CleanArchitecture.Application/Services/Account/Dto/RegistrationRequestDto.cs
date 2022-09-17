

namespace CleanArchitecture.Application.Services.Account.Dto
{
    public class RegistrationRequestDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
