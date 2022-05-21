namespace RoundUpFeature.Api.StarlingService;

public class StarlingApiClient : IApiClient
{
    private readonly HttpClient _httpClient;

    public StarlingApiClient(HttpClient httpClient)
    {
        if (httpClient is null)
        {
            throw new ArgumentNullException(nameof(httpClient));
        }
            
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> GetAsync()
    {
        return await _httpClient.GetAsync("test");
    }
}

