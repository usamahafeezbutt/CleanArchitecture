
namespace CleanArchitecture.Infrastructure.Identity.Models
{
    public class JwtSettings
    {
        public string Secret { get; set; } = null!;
        public int Expiry { get; set; }
    }
}
