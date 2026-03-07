using DishApi.Application.Dtos.Dishes;
using MiniValidation;

namespace DishesApi.EndpointFilters;

public class ValidateAnnotationsFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var dishForCreationDto = context.Arguments.FirstOrDefault(c => c?.GetType() == typeof(DishForCreationDto));

        if (dishForCreationDto == null)
        {
            return await next(context);
        }

        var isValid = MiniValidator.TryValidate(dishForCreationDto, out var validationErrors);

        if (!isValid)
        {
            return TypedResults.ValidationProblem(validationErrors);
        }

        return await next(context);
    }
}
