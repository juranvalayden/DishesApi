using System.Text.Json;

namespace DishesServer.Helpers;

public static class JsonSerializerWrapper
{
    public static JsonSerializerOptions Options => new(JsonSerializerDefaults.Web)
    {
        DefaultBufferSize = 10
    };
}
