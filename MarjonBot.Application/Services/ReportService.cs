using MarjonBot.Application.Interfaces;
using MarjonBot.Domain.Entities;

namespace MarjonBot.Application.Services;

internal sealed class ReportService : IReportService
{
    private readonly IReportGenerator _reportGenerator;

    public ReportService(IReportGenerator reportGenerator)
    {
        _reportGenerator = reportGenerator ?? throw new ArgumentNullException(nameof(reportGenerator));
    }

    public async Task<Stream> GenerateReportAsync()
    {
        var datas = GenerateMockReport();

        return await _reportGenerator.GenerateAsync(datas);
    }

    private List<Report> GenerateMockReport()
    {
        var reports = new List<Report>
    {
        new Report
        {
            Id = 1,
            CarNumber = "ABC123",
            CarModel = "Toyota Corolla",
            MilleageForTheWeek = 500,
            ContactPerKm = 5,
            NumberOfContactsPerWeek = 100,
            MonthlyPaymentAmount = 1000,
            CPM = 10
        },
        new Report
        {
            Id = 2,
            CarNumber = "DEF456",
            CarModel = "Hyundai Elantra",
            MilleageForTheWeek = 600,
            ContactPerKm = 4,
            NumberOfContactsPerWeek = 120,
            MonthlyPaymentAmount = 1100,
            CPM = 9
        },
        new Report
        {
            Id = 3,
            CarNumber = "GHI789",
            CarModel = "Chevrolet Malibu",
            MilleageForTheWeek = 450,
            ContactPerKm = 6,
            NumberOfContactsPerWeek = 90,
            MonthlyPaymentAmount = 950,
            CPM = 10
        },
        new Report
        {
            Id = 4,
            CarNumber = "JKL012",
            CarModel = "Honda Civic",
            MilleageForTheWeek = 520,
            ContactPerKm = 5,
            NumberOfContactsPerWeek = 110,
            MonthlyPaymentAmount = 1050,
            CPM = 9
        },
        new Report
        {
            Id = 5,
            CarNumber = "MNO345",
            CarModel = "Kia Forte",
            MilleageForTheWeek = 480,
            ContactPerKm = 7,
            NumberOfContactsPerWeek = 95,
            MonthlyPaymentAmount = 970,
            CPM = 10
        },
        new Report
        {
            Id = 6,
            CarNumber = "PQR678",
            CarModel = "Mazda 3",
            MilleageForTheWeek = 530,
            ContactPerKm = 4,
            NumberOfContactsPerWeek = 130,
            MonthlyPaymentAmount = 1200,
            CPM = 9
        },
        new Report
        {
            Id = 7,
            CarNumber = "STU901",
            CarModel = "Ford Focus",
            MilleageForTheWeek = 610,
            ContactPerKm = 6,
            NumberOfContactsPerWeek = 115,
            MonthlyPaymentAmount = 990,
            CPM = 8
        },
        new Report
        {
            Id = 8,
            CarNumber = "VWX234",
            CarModel = "Nissan Sentra",
            MilleageForTheWeek = 470,
            ContactPerKm = 5,
            NumberOfContactsPerWeek = 105,
            MonthlyPaymentAmount = 980,
            CPM = 9
        },
        new Report
        {
            Id = 9,
            CarNumber = "YZA567",
            CarModel = "Volkswagen Jetta",
            MilleageForTheWeek = 495,
            ContactPerKm = 5,
            NumberOfContactsPerWeek = 108,
            MonthlyPaymentAmount = 1010,
            CPM = 9
        },
        new Report
        {
            Id = 10,
            CarNumber = "BCD890",
            CarModel = "Subaru Impreza",
            MilleageForTheWeek = 550,
            ContactPerKm = 6,
            NumberOfContactsPerWeek = 112,
            MonthlyPaymentAmount = 1030,
            CPM = 9
        }
    };

        return reports;
    }

}
