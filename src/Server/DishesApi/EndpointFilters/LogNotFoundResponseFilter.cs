using System.Net;

namespace DishesApi.EndpointFilters;

public class LogNotFoundResponseFilter : IEndpointFilter
{
    private readonly ILogger<LogNotFoundResponseFilter> _logger;

    public LogNotFoundResponseFilter(ILogger<LogNotFoundResponseFilter> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);

        if (result is INestedHttpResult { Result: IStatusCodeHttpResult { StatusCode: (int)HttpStatusCode.NotFound } })
        {
            _logger.LogInformation("Resource {PathString} was not found.", context.HttpContext.Request.Path);
        }

        return result;
    }
}
