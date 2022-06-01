using Microsoft.Net.Http.Headers;
using RoundUpFeature.Api.Helpers;
using RoundUpFeature.Api.RoundUpService;
using RoundUpFeature.Api.StarlingBankService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IStarlingService, StarlingService>(httpClient =>
{
    const string accessToken = "eyJhbGciOiJQUzI1NiIsInppcCI6IkdaSVAifQ.H4sIAAAAAAAA_31U25KjIBD9lSmfhyk1aIxv-7Y_sB_QQJNQQbAAMzu1tf--KBhjJrWP55y-Hbr1T6G8L_oCRkUEDvbDB3BamTMDc_3gdijeCz-xGEElxROeOGG8PhLatJJAXdWkks2JyQaZgC4G4--x6Ku2oVVbnsrje6EgJKI80mYmgHM7mfDTaoHulxJz97IrqxIOhFHKCC0PjEAZu1RwEO0BDsBaHmsHe0WTM4417SQiYa3oCJVASSckJUhF3XZQAR5lzIi2fnCO3qesqoYjUmSkwjmrPtTRA8Y-TUebumVN27WzYW5HnJukSQnX1qPoHYJ4W7nLMj4xMOBLIXyNT4ISaIKSCt2e18qHHZOBEC4O3qNQ4Q6SEgLwy4D3yA1_OhXwDaZwsU75uEaijFA3JSbQKZiBBsPzaBycINya4KxOjWYma9ZI5QYIyhpiJZGTEf4u-Xv3FaTWfPLBDqtFHEDlwhrjIObcwzjqrztaogYwAgL2AjXGEivMmrtimI2MDiU6jLP7_0lpjKSNGjjGFwh4douPx8TvYk5Fxy-wuhswQJwGeh7homa8mBrhC3GVEsgmEtiCiBrgnD1lInZ2Zl1Lin-QgwPjgW9TR5qwSV_7dbu4UdsECW9DJLwWmG8k3tugwlZTWx6HeKiwEMTOR_LM5ixnpdKrpeRxRy1RDjmqMeyA30vpwZflOdBZ9HCLO_XkbLexdlw2t-O-Zab3i4VfldjEF7U2MRXlFxSTRkHy-2UaQ4h-pzHDEdZvKv5Al9sm1omH9nt27btnX-QT-2nufMBlgdzfnqlRyExNzHMXH3e-m7XLI7dEPR7Xsr_nayv-_gPpog3bGQYAAA.XPRdRd_3K-XXXPdI5Pj1qFwCk54ScEazC1nwsy2U2LM_dtSo7I39FzdbWV8236QDgbkElHlViotD35ya-q7ZDd_PTHNnAAyE08VwJQnt0Prx4AcieiUpzktoOgipyvRDxl8kAlwCjxFf1NLpqghNR2xgZKgbPUQlu9rL8QAiqL-gv_DUjkaUxfTm0acIDtdErVGdbvLXiPltoZLO-EGvvD7WQx-Vk9BXuhoxMCKBumffaMzmVpGacOfHViDuUPlCYV2OppOCtULp2wCp0BlnkwnecIQmuzf4pDXmvLLPhc9ph7_mqx3cZWSULnUaOy_0gg3KT4Z9BTJxcW-7QP-BoZBGWcybZnxFqd6uwRJuI2ijzug3YqYFz0ptsAWbIq4D5MJMVChHZVRPogHclMleTretJG6idUeBgO6VxSLHskKXJIBT69-yhhNOBEnseVh8nY-lKrl6lvAcqdnvGwST5h5SnvqbkaKI9ws8RgBpaxk0ax0dtsUl4n8EJRFvTDOFUTAeTu4KPnHTqrGdYQXjOG1FAdNjuhO1C_RBMCwKW90DaPkhUUO06bt3wrCleXyENIvVm3-fk_VlTdL11pxnTobP4RFx72ZP4Nm0KITZbb75RKPahtUc5_0yodDB13zkjlLLsmU_Zi-NTYyvdtmygpp2PeyuAKx5B_AKLJwMGfw";
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
