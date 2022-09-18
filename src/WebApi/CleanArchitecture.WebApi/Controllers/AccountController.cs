using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Application.Common.Models.Identity;
using CleanArchitecture.Application.Services.Account.Dto;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Security.Certificates;
using System.Net;

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
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequestDto request)
        {
            var result = await _accountService.AuthenticateAsync(request);
            return result is not null ? Unauthorized() : Ok(result);
        }

        [HttpPost(nameof(ChangePassword))]
        public async Task<ActionResult<AuthResponse>> ChangePassword([FromBody] ChangePasswordDto request)
        {
            var result = await _accountService.ChangePassword(request);
            return Ok(result);
        }

    }
}
