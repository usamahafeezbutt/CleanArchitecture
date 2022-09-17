

namespace CleanArchitecture.Application.Services.Customer.Dto
{
    public class CustomerCreateDto : CustomerBaseDto
    {
        public string Password { get; set; } = null!;
    }
}
