using Microsoft.Net.Http.Headers;
using RoundUpFeature.Api.Helpers;
using RoundUpFeature.Api.RoundUpService;
using RoundUpFeature.Api.StarlingBankService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IStarlingService, StarlingService>(httpClient =>
{
    const string accessToken = "eyJhbGciOiJQUzI1NiIsInppcCI6IkdaSVAifQ.H4sIAAAAAAAA_31Uy27bMBD8lUDnMLCetnXrrT_QD1iSS5sIRQoklTQo-u9diZRlOUaPM7OPWe5KfwodQtEXMGomcXBvIYI32l442Pc34YbitQgTp4hGNXjGs2BcVEfWtJ1iUJUVK1V75qpFLuFEwfh7LPqya-tj1VSH42uhISaiq4-nmQAh3GTjT2ck-l9azt0Pp0N5gJrxpuGsOdScwYG6lFDLroYaeCeodnTvaFPGiSjJUbJjezyxRrUVgxobxs9nVaoSW1QtZdBYP4TAEFJWWcERG-SsRElZVU1ZFVKf9tS0Vcfb7tTNAws34mwrOWXCuICy9wjyZeWui31mYcCnQvwaHwQt0UatNPo9b3SIOyYDKT0Z71HqeANJiRHEdcBb5IY_vY74AlO8Oq8DrZFpK_WHlhOYFMzBgBXZmgAvmXA2emdSo5nJmrNK-wGidpY5xdRkZbhJ4dZ9Bam1mEJ0wzoiDqBzYYNkxF56GEfzdUNL1ABWQsReokEqscKs-XeM8yCjR4UeyXv4n5RsJG00IJBeIOLFL3PcJ34Xcyp6cYV1ugEjkBvoBcFFzXgZaoQvxFVKIA-RwBbE9ACXPFMmqLO361pS_J0cPdgAYnNNNOOTee_X7eJGbQ4S3kwkvBaYb4TubdBxq2mcIBN3FRaCuflIHtmc5Z3SZh0pzbijliiPAvUYdyDspfTgy_I8mCwG-KCdBnZxm60dl4fbcd8y0_tR4WclNvFJrU1MRcUV5WToT5PfL9MYI807jRmOsH5T9ANdbps5L-_a79m17559ks_cp73xEZcFivDxSI1SZWriQXh63Plu1i733BJ1f1zL_h6vrfj7D7v0J_UZBgAA.vPzcBd6pg0dnzAxmpPNzmJGKADcdRVOlGR4B7_AWqmJmel1hSQrw1HffRCuMy10h6ywcy8V1_ZaMg6Do8NSNAycJq5XMUM2yAPWbJ7p5Gw7YlPSQFy38sSpoe62kn1vijZOzcsYjCpsw-25YMvF8BxMjcHz472hCHPtHnqh2LR6hYVhZL53oHSrp5tqELSAExexKws8IO15r6zXBeW7uPtuHEVneFkNS2lK7cnGxHptbCvvGRUqA3Zkn0M95Tv9bwo1oEQcYQLPyjCSfsv1NbvcJq4p_-gEL0EDsy0TzFX_blki4kEhOCzX35VWkrw7SaKyBTpl0uG9aFG45SfbsC2WSP-uR1lD_Awn-cS3YRyCupgRmR0iMZSfRmJqLLA1iR93eDYAK8ppxIepbssI0hekYe_9E_DaXVQSyLrkyIPzVHusf-9A2ah_DFzo3KUCwOpVWAucP31W4uo0ZVP49O_ZY0BU5cek_cDUGGyXF7k3fuJZzK83b0L14j4sge2v5qIZdnGGSLxHW2SHCuJKiCNHs7l9oYuh8xMUYDSjt3PPwZRRBiTRX8S9ons7IhhJuqG8Ppf_RE90qO-TSRNh4kScJPT29OaXN8X9goy_XCs6non95TSzqaJ67riVw17sMbzIIWI5_iOWVHt_4BrkSvT33GoUg1GNFX3GsQKNbyjA";
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
