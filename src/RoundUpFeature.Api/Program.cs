using Microsoft.Net.Http.Headers;
using RoundUpFeature.Api.Helpers;
using RoundUpFeature.Api.RoundUpService;
using RoundUpFeature.Api.StarlingBankService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IStarlingService, StarlingService>(httpClient =>
{
    const string accessToken = "";
    httpClient.BaseAddress = new Uri("https://test.com.apo");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.UserAgent, "OzgurTezel");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Authorization, $"Bearer {accessToken}");
});
builder.Services.AddScoped<IRoundUp, RoundUp>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global error handler
app.UseMiddleware<CustomErrorHandler>();

app.UseAuthorization();

app.MapControllers();

app.Run();
