using System.Text.Json;

namespace DishesClient.Application.Helpers;

public class JsonSerializerOptionsWrapper
{
    public JsonSerializerOptions Options { get; } = new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        DefaultBufferSize = 10
    };
}
