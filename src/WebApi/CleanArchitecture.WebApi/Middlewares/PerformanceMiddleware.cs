using CleanArchitecture.Application.Common.Interfaces.Identity;
using System.Diagnostics;

namespace CleanArchitecture.WebApi.Middlewares
{
    public class PerformanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        public PerformanceMiddleware(RequestDelegate next, ILogger logger, ICurrentUserService currentUserService)
        {
            _next = next;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                await _next(httpContext);
                stopwatch.Stop();
                if (stopwatch.ElapsedMilliseconds > 500)
                {
                    _logger.LogWarning($"Long Running Reqest taken almost {stopwatch.Elapsed.TotalSeconds} seconds"
                        + (!string.IsNullOrWhiteSpace(_currentUserService.UserId) ? $" by user Id {_currentUserService.UserId}" : "")
                        + $" from this Ip Address {httpContext.Connection.RemoteIpAddress}");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
