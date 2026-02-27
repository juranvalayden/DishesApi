using System.Text.Json;

namespace DishesClient.Runner.Helpers;

public static class JsonSerializerWrapper
{
    public static JsonSerializerOptions Options => new(JsonSerializerDefaults.Web)
    {
        DefaultBufferSize = 10
    };
}
