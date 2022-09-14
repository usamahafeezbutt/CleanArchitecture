using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Application.Services.Customer.Dto;

namespace CleanArchitecture.Application.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        public Task<Guid> AddCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDto> GetCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerDto>> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }
    }
}
