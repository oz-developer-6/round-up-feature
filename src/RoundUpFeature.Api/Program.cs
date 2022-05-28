using Microsoft.Net.Http.Headers;
using RoundUpFeature.Api.Helpers;
using RoundUpFeature.Api.RoundUpService;
using RoundUpFeature.Api.StarlingBankService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IStarlingService, StarlingService>(httpClient =>
{
    const string accessToken = "eyJhbGciOiJQUzI1NiIsInppcCI6IkdaSVAifQ.H4sIAAAAAAAA_31Uy5LbIBD8lS2dly29LeuWW34gHzDAYFNGoALkzVYq_x4kkGR5XTlO9zy6mZH-ZNK5rM9glITjYD6cB6ukvlDQtw9mhuw9cxMNGbWo8YxnRigrT6RuWkGgLEpSiOZMRYOUQxeS8feY9UXbVF1eVnX9nknwETgVzXkGgDEzaf_TKI72l-Tz9LzLixwqQuuakjqvKIE8TCmg4m0FFdCWhd7e3FDHik4wJqCtCNA6JzWjJ0Jz0ZGm44gomhMFESqCrR-MoXOxqijhhDVSUiDvSF1WZfCAYU7T1U3Z0qbt2tkwMyPOsqJSwpRxyHuLwN9W7LrIJxoGfEn4r_GJkBy1l0KiPeJKOn9AUsC5DcJ75NJvQWS8B3YdcMvc408rPb7B5K_GShfWSKTm8i75BComU1CgWZLGwHLCjPbWqDhoRhJntJB2AC-NJkYQMWnuNspt09cgjmaT82ZYLeIAMjVWGIToSw_jqL62aMkaQHPw2HNUGFqsYeLsDf1sZLQo0GLQ7v5HRRmRGxUwDC_g8WIXH4-F38lUipZdYXU3oIegBnoWwoVN8WJqhC_ElYpBMhGDPYnIAS7JUwLCZKvXtcT8B9pb0A7YrjrAhE7q1q_bxR3aFcR4FxHjtcF8I-HeBun3nsqwIOKhwwIQMx_JM5qqrBFSrZaixwO0ZFlkKEd_CNyRig--LM-CSqSDe9ipIxezyzpgydwB-1YZ3y80ftViJ1_02snYlF2RTwo5Se-XYPQ--J3GFI6wflPhB7rcNjGWP4w_ouvcI_qinphPveEelwUyd3-GRi4SNFHHbHjc-W7WKY_YkvV4XMv-nq8t-_sPYSTwKRkGAAA.uC3h64frVKUl5wZgpxt9pQjYVGTgU0LeA9fTV_LojLSGji3QSlBZSk7K8AOWeetAYICvK-n_8U1yb7v_pbcy7Au64RNU4PN7r1nYI5xfmrtYlP6QHYq_9RYAxm5hIaU1DRrJ9zDIANlYPENWIRf0UJTYXH0N8JnHG7TLJFruolrHU-bRQy3KKg2J-cQgsw460dk2E6br1UifhV0kAWczv1sWl1-XlYSaDRwsFX-rIO82afKyOJCiC614t9BfMTwv1gwG3ZG3Y0ujxnQZbLz7U2d_b4tVfysB_w1ufFB045D4avwBHgm9bA-_T0ozmQJjZH5xfplPxLR61hlASZxJfuwbgKgZj_ma5UITSWVsWp6z6obW5IOWxQSam28sGA14C-KUGrw5RVn9wwTlg51Chu5OAGIxZDBRwe_PBoofo2rq_rcWp4LQ8XGfSzsbWJCFsacr46rKW-3UP3l7XAm9aY7j1EQACIrV7o1mqEy8lxY6HYGmnFpNHoTm38JaBIKSbniAo53UKVo9SxNx49ja9f-FI7NKnMsb9FFsCHq7Dc-DLM_yiwC_gqPqKJ8uu5GLKkTbXWtcUcEKxnoUX4cubLUEw_Tgyi1b9tnWgA2pq6dIKFt1WbnOPaQi4G7i8EM49enq4PFRwFhRHeYQ4RhhBtki8h5PHpH8je7QVPZVlJs";
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
