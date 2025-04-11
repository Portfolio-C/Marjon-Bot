using MarjonBot.Application.Interfaces;
using MarjonBot.Domain.Entities;
using Syncfusion.XlsIO;

namespace MarjonBot.Application.Services;

internal sealed class ReportGenerator : IReportGenerator
{
    public async Task<Stream> GenerateAsync(List<Report> reports)
    {
        using var excelEngine = new ExcelEngine();
        IApplication application = excelEngine.Excel;
        application.DefaultVersion = ExcelVersion.Xlsx;
        IWorkbook workbook = application.Workbooks.Create(1);

        IWorksheet worksheet = workbook.Worksheets[0];
        worksheet.Name = "Haftalik hisobot";
        SheetFormat(worksheet, reports);

        var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return await Task.FromResult(stream);
    }

    private static void SheetFormat(IWorksheet worksheet, List<Report> reports)
    {
        // Title
        worksheet.Range["A1:H1"].Merge();
        worksheet.Range["A1"].Text = "Avtomobillar Haftalik Hisoboti";
        worksheet.Range["A1"].CellStyle.Font.Bold = true;
        worksheet.Range["A1"].CellStyle.Font.Size = 20;
        worksheet.Range["A1"].CellStyle.Font.FontName = "Verdana";
        worksheet.Range["A1"].CellStyle.Font.RGBColor = Syncfusion.Drawing.Color.FromArgb(0, 0, 112, 192);
        worksheet.Range["A1"].HorizontalAlignment = ExcelHAlign.HAlignCenter;

        // Column Headers
        worksheet.Range["A2:H2"].CellStyle.Color = Syncfusion.Drawing.Color.Yellow;
        worksheet.Range["A2:H2"].CellStyle.Font.Bold = true;
        worksheet.Range["A2:H2"].CellStyle.Font.FontName = "Verdana";
        worksheet.Range["A2:H2"].CellStyle.Font.Size = 10;

        worksheet.Range["A2"].Text = "Машины";
        worksheet.Range["B2"].Text = "модель";
        worksheet.Range["C2"].Text = "Пробег за неделю";
        worksheet.Range["D2"].Text = "1 км/контакт";
        worksheet.Range["E2"].Text = "Число контактов за неделю";
        worksheet.Range["F2"].Text = "ежемесячная сумма оплаты";
        worksheet.Range["G2"].Text = "Стоимость 1 контакта";
        worksheet.Range["H2"].Text = "CPM (Стоимость 1000 контактов)";

        int row = 3;
        foreach (var item in reports)
        {
            worksheet.Range[$"A{row}"].Text = item.CarNumber;
            worksheet.Range[$"B{row}"].Text = item.CarModel;
            worksheet.Range[$"C{row}"].Number = item.MilleageForTheWeek;
            worksheet.Range[$"D{row}"].Number = item.ContactPerKm;
            worksheet.Range[$"E{row}"].Number = item.NumberOfContactsPerWeek;
            worksheet.Range[$"F{row}"].Number = item.MonthlyPaymentAmount;
            worksheet.Range[$"G{row}"].Formula = $"=F{row}/E{row}";
            worksheet.Range[$"H{row}"].Number = item.CPM;

            worksheet.Range[$"A{row}:H{row}"].CellStyle.Font.FontName = "Verdana";
            worksheet.Range[$"A{row}:H{row}"].CellStyle.Font.Size = 10;

            // Alternating row background color
            if (row % 2 == 1)
            {
                worksheet.Range[$"A{row}:H{row}"].CellStyle.Color = Syncfusion.Drawing.Color.FromArgb(231, 239, 246);
            }

            row++;
        }

        worksheet.UsedRange.AutofitColumns();
    }

}
