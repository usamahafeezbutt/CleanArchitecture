using CleanArchitecture.Application.Services.Customer.Dto;

namespace CleanArchitecture.Application.Common.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetCustomers();
        Task<CustomerDto> GetCustomer(Guid customerId);
        Task<Guid> AddCustomer(CustomerDto customer);
        Task<bool> UpdateCustomer(CustomerDto customer);
        Task<bool> RemoveCustomer(Guid customerId);
    }
}
