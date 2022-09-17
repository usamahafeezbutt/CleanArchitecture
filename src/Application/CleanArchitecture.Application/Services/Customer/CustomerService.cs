using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Application.Services.Customer.Dto;

namespace CleanArchitecture.Application.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        public Task<Guid> AddCustomer(CustomerCreateDto customer)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerCreateDto> GetCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerCreateDto>> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCustomer(CustomerCreateDto customer)
        {
            throw new NotImplementedException();
        }
    }
}
