using Microsoft.Net.Http.Headers;
using RoundUpFeature.Api.Helpers;
using RoundUpFeature.Api.RoundUpService;
using RoundUpFeature.Api.StarlingBankService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IStarlingService, StarlingService>(httpClient =>
{
    const string accessToken = "eyJhbGciOiJQUzI1NiIsInppcCI6IkdaSVAifQ.H4sIAAAAAAAA_31U25KjIBD9lSmfh6moaIxv-7Y_sB_QQJNQQbAAMzu1tf--KBhjJrWPfU5fzqFb_xTK-6IvYFRE4GA_fACnlTkzMNcPbofivfATixlUUjzhiRPGqyOhTSsJVGVFStmcmGyQCehiMv4ei75sm5o2R3oq3wsFIQH1sTzMAHBuJxN-Wi3Q_VJinn7oDuUBasIoZYQeakbgEKeUUIu2hhpYy2PvYK9oUsXx1HQVaxmRooxqWN2QjjNKSo6C1UjhKFmsiLZ-cI7ep6qygiNSZKRE0RFa1VX0gHFO09GmalnTdu1smNsRZ1lJKeHaehS9QxBvK3ZZ5BMDA74kwtf4RCiBJiip0O1xrXzYITkQwkXhPQoV7kFiQgB-GfCeucWfTgV8gylcrFM-rpEoI9RNiQl0SmagwfAsjYMThFsTnNVp0Ixkzhqp3ABBWUOsJHIywt8pf5--Bmk0n3yww2oRB1C5scYoxJx7GEf9dY-WrAGMgIC9QI2xxRpmzl0xzEZGhxIdRu3-f1SSkbhRA8f4AgHPbvHxWPidzKXo-AVWdwMGiGqg5zFc2Bwvpkb4QlypFGQTKdiSiBrgnD1lIE52Zl1Lyn-ggwPjgW-qI0zYpK_9ul3coE1BijcRKV4bzDcS721QYeupLY8iHjosALHzkTyjucpZqfRqKXncQUuWQ45qDLvA76n04MvyHOhMerjFnXpytpusHZbN7bBvlen9YuNXLTbyRa-NTE35BcWkUZD8fhnGEKLfaczhCOs3FX-gy20T68TD-D26zt2jL-qJ_TR3POCyQO5vz9AoZIYm5rmLjzvfzTrlEVuyHo9r2d_ztRV__wEdrcURGQYAAA.tIRbL2-IQXCCimgttUnUMe_fItkRaCR3y_NKL5u1OQp7HbKISSuiqOHAsE0BI4VEgreHuXkg3RMtKu5DByWLSs1yDZU2SnqVRTMXRCf9G6vmx3VJPQcZXTDhF-QNQ96BAWa1NG9d6d6O4KKyrn5BYIn5lKvOwd2AgkEMj8Qg49CaT-Lm-OFi0qPm71DVuI_LF1efilrZgkELXzuEuYkwmmomQzWTWzGwmEIV9oL9diYlYI8bYd8gsbxZJ5_Xa7UtIBhGd-7viXWmv1swBa-izXFWi8bHUeqPQPg17QjoK2RoVsHmvsDFeEC6sQY4fa15fJSIeRrPZExC3ZuEn5IXOfIUwfEWjBYwjBecvQIpitLKL_CGEzAz4iJdMRckVEUj4DSeKsGnm30zD20rvPdpyIpL6Dlw4Xs0IObKn94p__7y3YKNY_uv0evkYkK5FDl0seNNQlYNx7snTUWx3EJ34B8mkzAxXLsDHimr53VLw7s9EeFwPEBa-VjLcCCjI9lV1Wgq-EFMoCbMRFPOOQoTQng02ZJpDSPyb3lhX3gAoFNkdv7_1gYLfOOriJHQgYaIpZQVD-J_5SdDgJzGl1qauCQdP42pdmSHwmQILUNclacWXEI3af0Don9D03y5HbO9Jj77P5mJvSX8JDLMibIsv21S2DWL0j6SLq39U80uAxU";
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
