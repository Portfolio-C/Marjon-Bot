using MarjonBot.Application.Extensions;
using MarjonBot.Application.Interfaces;
using MarjonBot.Domain.Entities;
using MarjonBot.Domain.Responses;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace MarjonBot.Application.Services;
internal class ApiService(HttpClient httpClient, IConfiguration configuration) : IApiService
{
    public async Task<List<Report>> GetReports(long userId)
    {
        var cars = await GetUserCarsList();
        //var carLogs = await GetCarLogsListAsync();

        var reports = new List<Report>();

        foreach (var car in cars)
        {
            //  var log = carLogs.FirstOrDefault(x => x.CarId == car.Id);

            int mileage = 12345;
            int contactPerKm = 150; // Simulated contact per km, can be replaced with actual logic
            double monthlyPayment = 150000; // Simulated monthly payment, can be replaced with actual logic

            reports.Add(new Report
            {
                UserId = userId,
                Id = car.Id,
                CarNumber = car.StateNumber,
                CarModel = car.Model.Title,
                MilleageForTheWeek = mileage,
                ContactPerKm = contactPerKm,
                NumberOfContactsPerWeek = contactPerKm * mileage,
                MonthlyPaymentAmount = monthlyPayment,
                CostOfOneContact = (decimal)monthlyPayment / (contactPerKm * mileage),
            });
        }

        return reports;
    }

    public async Task<bool> LoginAsync(LoginDto request)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("https://marjon.uz/api/app/v1/users/Login/", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Login failed: {error}");
            Console.ResetColor();

            return false;
        }

        await UpdateTokenAsync(response);

        return true;
    }

    public async Task<List<Car>> GetUserCarsList()
    {
        TokenManager.AddAuthorizationHeader(httpClient, configuration);

        var response = await httpClient.GetAsync("https://marjon.uz/api/app/v1/cars/MyCarList/");
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var myCars = JsonConvert.DeserializeObject<MyCarListResponse>(jsonResponse);

        return myCars.Cars;
    }

    public async Task<List<CarLogsListResponse>> GetCarLogsListAsync()
    {
        TokenManager.AddAuthorizationHeader(httpClient, configuration);

        var response = await httpClient.GetAsync("https://marjon.uz/api/app/v1/cars/CarLogsList/");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var cars = JsonConvert.DeserializeObject<List<CarLogsListResponse>>(json);

        return cars ?? [];
    }

    private async static Task UpdateTokenAsync(HttpResponseMessage response)
    {
        var responseString = await response.Content.ReadAsStringAsync();
        var responseJson = JsonConvert.DeserializeObject<LoginResponse>(responseString)
            ?? throw new Exception("Failed to deserialize login response");

        await TokenManager.UpdateAccessTokenAsync(responseJson.Token, "appsettings.json");
    }
}