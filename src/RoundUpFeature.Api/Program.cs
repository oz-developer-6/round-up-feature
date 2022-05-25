using Microsoft.Net.Http.Headers;
using RoundUpFeature.Api.Helpers;
using RoundUpFeature.Api.RoundUpService;
using RoundUpFeature.Api.StarlingBankService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IStarlingService, StarlingService>(httpClient =>
{
    const string accessToken = "eyJhbGciOiJQUzI1NiIsInppcCI6IkdaSVAifQ.H4sIAAAAAAAA_31Uy5KbMBD8lS3OO1vmacwtt_xAPmAkDbbKQqIk4c1WKv8egYQxXleO0z2Pbs3An0w6l3UZjhIEDebDebRK6jNDff3gZsjeMzexkFH1FZ3oxIHx4ghV3fSARV5A3tcn1tfEBLYhmX6PWZc3dVnXRV4W75lEH4GqqY8zgJybSfufRgmyv6SYpx_aQ37AElhVMagOJQM8hCk5lqIpsUTW8NDbmyvpWHHs26IQbQFNKRCqY9tCW9YcTpzK6lTQocYyVARbPzgn52JVXuCRKmKQk2ihKsoieKAwp26rumhY3bTNbJibkWZZUSlwZRyJzhKKtxW7LPJB40AvCf81PhFSkPayl2T3uJLO75AUCGGD8I6E9PcgMt4jvwx0z9ziTys9veHkL8ZKF9YIUgt5k2JCFZMZKtQ8SeNoBXCjvTUqDpqRxBndSzugl0aD6aGftHB3yt2nr0EczSfnzbBapAFlaqwoCNHnDsdRfd2jJWtALdBTJ0hRaLGGibNX8rOR0VJPloJ29z8qyojcqJBTeAFPZ7v4eCz8TqZSsvyCq7uBPAY12PEQLmyKF1MjfhGtVAySiRhsSSAHPCdPCQiTrV7XEvMfaG9RO-Sb6gADm9S1W7dLG7QpiPEmIsZrg_lGwr0N0m89leFBxEOHBQAzH8kzmqqs6aVaLUWPO2jJssRJjn4XuD0VH3xZnkWVSIe3sFMHZ7PJ2mHJ3A77VhnfLzR-1WIjX_TayNiUX0hMigSk90sweR_8TmMKR1y_qfADXW4bjBUP4_foOnePvqgH86nvuKdlgdzdnqFR9AmamOM2PO58N-uUR2zJejyuZX_P15b9_QdRE7UfGQYAAA.wfmBh7vXVqolmUCDsMKdFh_SnxNbtOQc9AUIkVDnZ5MquqHeHEXC4oCjro5CF72zKnGyxPNjBybGgZyQDc2Vj6U8LMjdUpHICPu-WxgwB8zrGg-ZVcb4JLF082v5FZxIuh-GdaFEWypvmZ3HcRz47MmPW0iKsuD2LaOt5emKd5ah_JEmkjsoAMewOSHEfF4ZGpv3_BQedSTRfDRiGyjrOAfZrY0UIDl_6CDLkbnxso-IOY-1f0IBtM-Bq453hwpI3l47Kd4tYunLWJjJFCeNyBj0MNHf2_hHWaSvwLIi_oZYWAkfHtz5GyJTQOAs3ED2EKVP8iMh7Zka-poJfuFWq9iEuvhoCvOX5E8WUjcmgFOtZuQjpKERmlbG4DYi1S3b21an3NtwjTZH0qQcSRMECv-0om6m9bydYtSJaRgCBVG7l5IeMBtHaOpEb7Ft-g74T6MJVJViCvaAlUytLluWBD8yqe09YqGxkVHORBFmdc4GqkUkERDZJi0Zz70Fxw3vn6Ha0oos6cSxqe8-vjQ7lwMloHacceQnuShz9IkXAJ4rRadJUf77tjTJ5Ogv_JHvq7I6yMheTQsi14mGW_PoyanScytlVUHNNfcOUKR31LXSdfpkzRbA3o5OLRxrfO7CYIpl18WAk50-eUG7FlXdGlR5v2X87FHfkHzcPDdT_nA";
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
