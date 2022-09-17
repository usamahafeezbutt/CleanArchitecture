

namespace CleanArchitecture.Application.Services.Customer.Dto
{
    public class CustomerBaseDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Address { get; set; } 
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
