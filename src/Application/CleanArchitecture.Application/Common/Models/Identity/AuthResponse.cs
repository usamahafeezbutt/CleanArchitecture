
namespace CleanArchitecture.Application.Common.Models.Identity
{
    public class AuthResponse
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public int Expiry { get; set; }
    }
}
