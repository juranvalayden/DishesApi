using System.Text.Json;

namespace DishesServer.Application.Helpers;

public class JsonSerializerOptionsWrapper
{
    public JsonSerializerOptions Options { get; } = new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        DefaultBufferSize = 10
    };
}
