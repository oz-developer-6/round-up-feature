namespace RoundUpFeature.Api.StarlingService
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> GetAsync();
    }
}
