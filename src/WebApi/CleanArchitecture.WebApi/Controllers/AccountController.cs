using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Application.Common.Models.Identity;
using CleanArchitecture.Application.Services.Account.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost(nameof(Register))]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegistrationRequestDto request)
        {
            var result = await _accountService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost(nameof(Authenticate))]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequestDto request)
        {
            var result = await _accountService.AuthenticateAsync(request);
            return Ok(result);
        }

        [HttpPost(nameof(ChangePassword))]
        public async Task<ActionResult<AuthResponse>> ChangePassword([FromBody] AuthRequestDto request)
        {
            var result = await _accountService.AuthenticateAsync(request);
            return Ok(result);
        }

    }
}
