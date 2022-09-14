using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        private ILogger<T>? _logger;
        protected ILogger<T> Logger => _logger ?? HttpContext.RequestServices.GetService<ILogger<T>>()!;

    }
}
