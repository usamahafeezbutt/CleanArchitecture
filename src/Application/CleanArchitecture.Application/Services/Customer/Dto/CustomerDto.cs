

namespace CleanArchitecture.Application.Services.Customer.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int Address { get; set; }
        public int Phone { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}
