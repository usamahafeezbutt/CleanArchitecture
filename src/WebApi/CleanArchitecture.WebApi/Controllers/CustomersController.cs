using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Application.Services.Customer;
using CleanArchitecture.Application.Services.Customer.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseController<CustomersController>
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            IEnumerable<CustomerDto> customers = await _customerService.GetCustomers();
            return Ok(customers);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CustomerDto>> Get(Guid Id)
        {
            CustomerDto customer = await _customerService.GetCustomer(Id);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CustomerDto customerDto)
        {
            Guid customerId = await _customerService.AddCustomer(customerDto);
            return Ok(customerId);
        }

        [HttpPut]
        public async Task<ActionResult> Update(CustomerDto customerDto)
        {
            bool customerUpdated = await _customerService.UpdateCustomer(customerDto);
            return Ok(customerUpdated);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(Guid Id)
        {
            bool customerDeleted = await _customerService.RemoveCustomer(Id);
            return Ok(customerDeleted);
        }

    }
}
