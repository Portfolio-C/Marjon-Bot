﻿using Bogus;
using MarjonBot.Application.Interfaces;
using MarjonBot.Domain.Entities;

namespace MarjonBot.Application.Services;

internal sealed class ReportService(IReportGenerator reportGenerator, IApiService apiService) : IReportService
{
    private readonly Faker _faker = new("ru");
    public async Task<Stream> GenerateReportAsync(long userId)
    {
        var datas = await apiService.GetReports(userId);

        return await reportGenerator.GenerateAsync(datas);
    }

    private List<Report> GenerateMockReport(long userId)
    {
        var reports = new List<Report>();

        for (var i = 1; i <= 20; i++)
        {
            reports.Add(new Report
            {
                UserId = userId,
                Id = i,
                CarNumber = _faker.Vehicle.Vin(),
                CarModel = _faker.Vehicle.Model(),
                MilleageForTheWeek = _faker.Random.Int(400, 3000),
                ContactPerKm = _faker.Random.Int(50, 300),
                NumberOfContactsPerWeek = _faker.Random.Int(5000, 50000),
                MonthlyPaymentAmount = _faker.Random.Double(10000, 100000),
            });
        }

        return reports;
    }

}
