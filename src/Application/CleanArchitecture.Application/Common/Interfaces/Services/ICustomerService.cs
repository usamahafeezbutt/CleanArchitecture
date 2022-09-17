using CleanArchitecture.Application.Services.Customer.Dto;

namespace CleanArchitecture.Application.Common.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerCreateDto>> GetCustomers();
        Task<CustomerCreateDto> GetCustomer(Guid customerId);
        Task<bool> UpdateCustomer(CustomerCreateDto customer);
        Task<bool> RemoveCustomer(Guid customerId);
    }
}
