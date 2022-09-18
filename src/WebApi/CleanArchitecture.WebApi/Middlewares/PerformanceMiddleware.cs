using CleanArchitecture.Application.Common.Interfaces.Identity;
using System.Diagnostics;

namespace CleanArchitecture.WebApi.Middlewares
{
    public class PerformanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerformanceMiddleware> _logger;
        public PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, ICurrentUserService currentUserService)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                await _next(httpContext);
                stopwatch.Stop();
                if (stopwatch.ElapsedMilliseconds > 500)
                {
                    _logger.LogInformation($"Long Running Reqest taken almost {stopwatch.Elapsed.TotalSeconds} seconds"
                        + (!string.IsNullOrWhiteSpace(currentUserService!.UserId) ? $" by user Id {currentUserService.UserId}" : "")
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
