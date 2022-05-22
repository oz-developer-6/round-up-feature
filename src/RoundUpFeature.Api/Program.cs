using Microsoft.Net.Http.Headers;
using RoundUpFeature.Api.Helpers;
using RoundUpFeature.Api.RoundUpService;
using RoundUpFeature.Api.StarlingBankService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IStarlingService, StarlingService>(httpClient =>
{
    const string accessToken = "eyJhbGciOiJQUzI1NiIsInppcCI6IkdaSVAifQ.H4sIAAAAAAAA_31Uy5LbIBD8lS2dly29beuWW34gHzDAYFNGQAHyZiuVfw8SyLK8rhynex7dzEh_Cul9MRRgJeE4mg8fwCmpzxT09YOZsXgv_ERjRitaPOGJEcrqA2m7XhCoq5pUojtR0SHlcIzJ-NsWQ9V3TVOd6v7wXkgICaib-jgDwJiZdPhpFEf3S_J5enksqxIaQtuWkrZsKIEyTqmg4X0DDdCexd7BXFGnikPZnXreAymbWpBWlDWBPpaVpWhqqLA9VTxWRFs_GEPvU1VVwwFbpKRCfiRtVBQ9YJzTHduu7mnXH_vZMDMWZ1lJKWHKeOSDQ-BvK3ZZ5BMNI74kwpd9IiRHHaSQ6Pa4kj7skBxw7qLwAbkM9yAxIQC7jHjP3OJPJwO-wRQuxkkf10ik5vIm-QQqJVNQoFmWxsBxwowOzqg0aEYyZ7SQboQgjSZGEDFp7u-Uv09fgzSaTT6YcbWII8jcWGEUos8DWKu-7tGSNYLmEHDgqDC2WMPMuSuG2Yh1KNBh1O7_RyUZibMKGMYXCHh2i4_Hwu9kLkXHLrC6GzFAVAMDi-HC5ngxZeELcaVSkE2kYEsicoRz9pSBONnpdS0p_4EODrQHtqmOMKGTug7rdnGDNgUp3kSkeG0w30i8t1GGracyLIp46LAAxMxH8ozmKmeEVKul5HEHLVkOGUobdoHfU-nBl-U5UJn0cIs79eRsNlk7LJvbYd8q0_vFxq9abOSLXhuZmrIL8kkhJ_n9MowhRL-TzaGF9ZuKP9Dltolx_GH8Hl3n7tEX9cR86jsecFkg87dnyHKRoYl65uLjznezTnnElqzH41r293xtxd9_087SoxkGAAA.lbI0elwtFMobcOvYBfl5PhkvLGnCu3ka5ejMFoD5sfkdeUQSX-BYRnISc4W9ReEbm4GfXwfg5D86knA6Y-d7plMHmhSYFaBG4b0I3PaO3SwBvedNHyGkBgwz5nKbaNDU_HzWZX9S1BpFoheXr4_pSOfdV1fL73Ds66V8945o6PbFwhbXRzMGrUsAnFgFYEzmZ0SjxAhDvlwuhPpBzVvyiQdZzCGnvbf5Esh8huKl0c3EBAO8CvjBkepZzsEoKripw93EEtjwkL3aKCXAY8OZ5CjbGa75aIovtGrvHOvayYJvfiXpcOR1fMOcxY6gwrpqWMF-SQl1GSHH9X-zzVaSnpgBtDiDLfiTZgpxB8Y-o5UxlBgPHXAM9-bJ4bUIsg8gAqjtlAZnHszCyyLvMAm4Whfm8el9s5rR0ubLdJydFOzqVV2LocM_gNzZTyy7OxQzhMKPkvs0EFSXudY3fie84SaE4kPbnLfio4RIdeUpGCTHPBhGtOgroqGvFduVZ_WafqW-dEmHnzuL6yPWpPznbGf91-fCjfrlUoCHQ0eb68jkJ_BWhn7NfTiZ-IHix_f9Yf6YVtIhw8Y_iolmkh_3EsxUgZ0j1E6OKlYEVtxYGpxtJpSDuCTGbaodshxKt_gCQMuf7OZGAb8AFbhw4utVwScEv4rDgDxtq7S3vUhzrc8";
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
