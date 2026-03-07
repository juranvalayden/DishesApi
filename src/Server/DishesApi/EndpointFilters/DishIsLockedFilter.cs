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
        Guid? dishId = null;

        if (context.HttpContext.Request.Method == "PUT")
        {
            dishId = context.GetArgument<Guid>(2);
        }

        if (context.HttpContext.Request.Method == "DELETE")
        {
            dishId = context.GetArgument<Guid>(1);
        }

        if (!dishId.HasValue)
        {
            throw new NotSupportedException("This filter is not supported for this scenario.");
        }

        var dishIdThatShouldNotBeAllowedToBeUpdated = new Guid("fd630a57-2352-4731-b25c-db9cc7601b16");

        if (dishId == dishIdThatShouldNotBeAllowedToBeUpdated)
        {
            // block the request from going forward
            return TypedResults.Problem(new()
            {
                Status = 400,
                Title = "Dish is perfect and we do not want it to be updated.",
                Detail = "Perfection cannot be updated :)"
            });
        }

        // invoke the next filter
        var result = await next.Invoke(context);
        return result;
    }
}
