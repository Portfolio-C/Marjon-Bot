using MarjonBot.Application.Interfaces;
using MarjonBot.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MarjonBot.Application.Services;
internal class ApiService(HttpClient httpClient, IConfiguration configuration) : IApiService
{

    public async Task<List<Report>> GetCarLogsAsync()
    {
        var response = await httpClient.GetAsync("https://marjon.uz/api/app/v1/users/Login/");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        // Report bu o'zim excelga moslab yozgan modelim API dan malumotlarni topolmaganim uchun 
        return JsonConvert.DeserializeObject<List<Report>>(content) ?? [];
    }

    public Task<Report> GetReports(long userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> LoginAsync(LoginDto request)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(configuration["Api:Token"]!);

        var json = JsonConvert.SerializeObject(request);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("https://marjon.uz/api/app/v1/users/Login/", content);

        response.EnsureSuccessStatusCode();

        return response.IsSuccessStatusCode;
    }
}
