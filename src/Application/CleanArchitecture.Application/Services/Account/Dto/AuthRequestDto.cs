﻿

namespace CleanArchitecture.Application.Services.Account.Dto
{
    public class AuthRequestDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
