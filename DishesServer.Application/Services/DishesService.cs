using DishesServer.Application.Dtos.Dishes;
using DishesServer.Application.Helpers;
using DishesServer.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Xml.Serialization;

namespace DishesServer.Application.Services;

public class DishesService : IDishService
{
    private readonly ILogger<DishesService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptionsWrapper _jsonSerializerOptionsWrapper;
    private readonly CancellationToken _cancellationToken = new (false);

    public DishesService(ILogger<DishesService> logger, IHttpClientFactory httpClientFactory, JsonSerializerOptionsWrapper jsonSerializerOptionsWrapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _jsonSerializerOptionsWrapper = jsonSerializerOptionsWrapper;
    }

    public async Task RunAsync()
    {
        var dishes = await GetDishesAsync(_cancellationToken);

        if (dishes.ToList().Count > 0)
        {
            _logger.LogInformation("The number of dishes return where {Count}", dishes.Count);
        }
        else
        {
            _logger.LogError("Error occurred in the retrieval of dishes.");
        }
    }

    public async Task<ICollection<DishDto>> GetDishesAsync(CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient(Constants.DishClient);

        // Normally I would configure this in the DI
        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.Json));
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.Xml, Constants.XmlQuality));

        var response = await httpClient.GetAsync(Constants.DishesEndpoint, cancellationToken);
        response.EnsureSuccessStatusCode();

        if (!response.IsSuccessStatusCode)
        {
            var statusCode = (int) response.StatusCode;
            _logger.LogError("Response returned the following: {StatusCode} which is {StatusMessage}.", statusCode, response.StatusCode);
            return [];
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(content))
        {
            _logger.LogError("No content found in the response.");
            return [];
        }

        var mediaType = response.Content.Headers.ContentType?.MediaType;

        if (string.IsNullOrWhiteSpace(mediaType))
        {
            _logger.LogError("No content media type can be found");
            return [];
        }

        List<DishDto>? dishes;

        switch (mediaType)
        {
            // check which is returned, json or xml
            case Constants.Json:
                dishes = JsonSerializer.Deserialize<List<DishDto>>(content, _jsonSerializerOptionsWrapper.Options);
                break;
            case Constants.Xml:
                var stringReader = new StringReader(content);
                var xmlSerializer = new XmlSerializer(typeof(List<DishDto>));
                dishes = xmlSerializer.Deserialize(stringReader) as List<DishDto>;
                break;
            default:
                dishes = null;
                break;
        }

        if (dishes != null) return dishes;

        _logger.LogWarning("No dishes found.");
        return [];
    }
}
