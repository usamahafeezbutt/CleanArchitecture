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
    public class CustomerController : BaseController<CustomerController>
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerCreateDto>>> GetAll()
        {
            IEnumerable<CustomerCreateDto> customers = await _customerService.GetCustomers();
            return Ok(customers);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CustomerCreateDto>> Get(Guid Id)
        {
            CustomerCreateDto customer = await _customerService.GetCustomer(Id);
            return Ok(customer);
        }

        [HttpPut]
        public async Task<ActionResult> Update(CustomerCreateDto customerDto)
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
