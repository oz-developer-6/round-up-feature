using Microsoft.Net.Http.Headers;
using RoundUpFeature.Api.Helpers;
using RoundUpFeature.Api.RoundUpService;
using RoundUpFeature.Api.StarlingBankService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IStarlingService, StarlingService>(httpClient =>
{
    const string accessToken = "eyJhbGciOiJQUzI1NiIsInppcCI6IkdaSVAifQ.H4sIAAAAAAAA_31Uy27bMBD8lUDnbGA9KNu69dYf6AcsyZVNhCIFkkoaFP33UiJlWY7R487sY4a70p9CeV90BY4KJA32zQd0WpkLR_P-JuxQvBZ-4jGj6Rs601kAF9URGtb2gFVZQdmzM-8ZcYmnmEy_x6IrW1bXjLGKvRYKQwKq9lzOAAphJxN-Wi3J_VJynn44HcoD1sCbhkNzqDngIU4psZZtjTXyVsTewb6TSRXHijM8Hzi0LWuhqXsJZzpyqGXTto0QTDIeK6KtH0KQ96mqrPBIDXEoSZ6gqeoqeqA4h50aVrWctad2NizsSLOspBSEtp5k5wjly4pdF_lgcKCnRPgaHwglyQTVK3J7XCsfdkgOpHRReEdShVuQmBBQXAe6ZW7xp1OBXnAKV-uUj2sEZaT6UHJCnZI5ajQiSxPoJAhrgrM6DZqRzFnTKzdgUNaA7aGfjPQ3yt-mr0EaLSYf7LBapAFVbqwpCjGXDsdRf92iJWtAIzFQJ0lTbLGGmXPvFGYjo6OeHEXt_n9UkpG4UaOg-AKBLm7xcV_4ncyl5MQVV3cDBYxqsBMxXNgcL6ZG_CJaqRRkEynYkkANeMmeMhAnO7OuJeXf0cGh8Sg21REGPun3bt0ubdCmIMWbiBSvDeYbifc2qLD11FZEEXcdFgDsfCSPaK5ytld6tZQ87qAly5EgNYZd4PdUevBleQ51Jj1-xJ16uNhN1g7L5nbYt8r0frHxsxYb-aTXRqam4kpy0iQhv1-GKYTodxpzOOL6TcUf6HLbYJ28G79H17l79Ek92E9zwwMtCxT-4xEaZZ-hiXvh4uPOd7NOuceWrPvjWvb3eG3F338j6P0nGQYAAA.gcJn6K68plpjucH1tWdGJqpSytbXKtkcBH0WsxCaBcxiVHCVjoRssdrepKkpArpDqls2l495Gj-boBD57aG1de96YBha585oMjf6swCGzLeZeuWBWwshv4NfLm-iTZjZh_a6CGPGdbPcLwM_TK_9_pMrfn3g69O4Kns3Pte5fWOc5sjCvgR08nBf49AX8lqJub0r7sA5FcoFZO7SO3S8Y1ZDEGS8Xv07EjQfmY88QpCMAzYEiV2hHhCR3HkuH1hQmXLYBVGP16HHb3R7oOkfOXzl8xrkchJPy_T405f62QbsJt_hp5148F1n1EAfCbY4w6lpTrhHYDheqbqFY9sC4EzpkRk0uW_T8YNf_ATGqBaFeGfENoMwJsfBamW4g4faSiyTc5hKDvmizwROmgMbR_1DZZIceDh_8z1MqXmgIl32hy_Ke3sLhAQSf7kC2pOmCgCqSrTd39KlZNyQwE7cnCOaUFASHGzf_xQtWDyP3Yy-mSgLmd270SNHgiwkgU7bwyUS1ymp82zv2cw7VQvgN_pVMu9MelL8AHMqs7gJMeUMKJYFWsC--8LQI-Ylul2X-lLkk1WcxUblliU6iXiF9KO6CESIcOXONZ14XQX5BZiXTrWFnMO6-dSTColXFL5pg_8wgIzrFH0OhnalZtRjqnJBr8hhvhFz7ooMaqbXp0Y";
    httpClient.BaseAddress = new Uri("https://api-sandbox.starlingbank.com");
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
