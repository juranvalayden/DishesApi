namespace DishesApi.EndpointFilters;

public class DishIsLockedFilter : IEndpointFilter
{
    private readonly Guid _lockedDishId;

    public DishIsLockedFilter(Guid lockedDishId)
    {
        _lockedDishId = lockedDishId;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var dishId = context.HttpContext.Request.Method switch
        {
            "PUT" => context.GetArgument<Guid>(2),
            "DELETE" => context.GetArgument<Guid>(1),
            _ => throw new NotSupportedException("This filter is not supported for this scenario.")
        };

        if (dishId == _lockedDishId)
        {
            return TypedResults.Problem(new()
            {
                Status = 400,
                Title = "Dish is perfect and cannot be changed.",
                Detail = "You cannot update or delete perfection."
            });
        }

        // invoke the next filter
        var result = await next.Invoke(context);
        return result;
    }
}