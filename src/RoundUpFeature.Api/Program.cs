using Microsoft.Net.Http.Headers;
using RoundUpFeature.Api.Helpers;
using RoundUpFeature.Api.RoundUpService;
using RoundUpFeature.Api.StarlingBankService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IStarlingService, StarlingService>(httpClient =>
{
    const string accessToken = "eyJhbGciOiJQUzI1NiIsInppcCI6IkdaSVAifQ.H4sIAAAAAAAA_31Uy5LbIBD8lS2dly29LemWW34gHzDAYFNGoALkzVYq_x4kkGV5XTlO9zy6mZH-ZNK5bMhgkoTjaD6cB6ukPlPQ1w9mxuw9czMNGbWosceeEcrKE6mbVhAoi5IUoumpaJBy6EIy_p6yoWibqu_6um_fMwk-AXmVLwAwZmbtfxrF0f6SfJmed3mRQ0VoXVNS5xUlkIcpBVS8raAC2rLQ25sr6lgRRlLosSb8hA2pBRYE2p4SFIKeOih7rE6hItj6wRg6F6uKEk5YIyUF8o7UZVUGDxjmNF3dlC1t2q5dDDMz4SIrKiVMGYd8sAj8bcMuq3yiYcSXhP-angjJUXspJNojrqTzByQFnNsgfEAu_T2IjPfALiPeM_f400qPbzD7i7HShTUSqbm8ST6DiskUFGiWpDGwnDCjvTUqDlqQxBktpB3BS6OJEUTMmrs75e7TtyCOZrPzZtws4ggyNVYYhOjzANOkvu7RmjWC5uBx4KgwtNjCxNkr-sXIZFGgxaDd_Y-KMiI3KWAYXsDj2a4-Hgu_k6kULbvA5m5ED0ENDCyEK5vi1dQEX4gbFYNkIgZ7EpEjnJOnBITJVm9rifkPtLegHbBddYAJndV12LaLO7QriPEuIsZbg-VGwr2N0u89lWFBxEOHFSBmOZJnNFVZI6TaLEWPB2jNsshQTv4QuCMVH3xdngWVSAe3sFNHzmaXdcCSuQP2rTK-X2j8qsVOvui1k7EpuyCfFXKS3i_B6H3wO08pnGD7psIPdL1tYix_GH9Et7lH9EU9MZ_6jntcF8jc7RmauEjQTB2z4XGXu9mmPGJr1uNxrft7vrbs7z_9bU_tGQYAAA.sjSqG00YdELkzbrSTdhBtidyItEwBtfQ4Ep4TmXWhmaSKgqlcbbSPe93emwJjqX_D0eR_0Z53R2pyrqsTZ9bEoODQXzB3gELCfDKDREC_8ZL_1VjuzD5OU-0dfVYMb-pJpehYiS3idzWf4QwBcGUjTyP_eUvVllx-6DZz38HgJPTOAm8Ld4rPV50NDK-qmyuZFibdzlow0VtlmqeexXdcnQxTfi_GFpG2cMh8VYKG9SLPbc7osdqGs9Xw-ytAlUTqGdJx3z-C772xaeEnQBLKjR4kf5331DRZOCb2I2zFMPaPrVy69Z1fuWzBjpre05g0I3zBqa3N8ZYCJ_9GShZkPjPWOfdZFaj8wcSGP1GKCCSeLJl5ik3T0joSmqiBpffYHLtBIFiP3vQJqc8i6eFR0OVJRu0Kv3i_FSRgyrDJfqydmWMjeLZMeBOL2cfIz-dEzGMXdYoW_i-o02wG14fcP_mzbfsmwmDnUt9tpR9wFvEeRbdcgw1F9MmlIFZgZcdLFeku19-shyQf9l_AA4M0LSHsgmzox0kmgXMIOcMtv2O_hKB_EcLShX3n1DTRs728KnvBuVI6jgeQMStDEEXQw_pxv24MsnAgbIzBAljpvTMKFIcnAIh4xqCZLEyAsMbVHwJB5q3Z4CY2ZMfHV8MUTE6tCG_358NFh1LIu8V8nY";
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
