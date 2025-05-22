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
        worksheet.SetColumnWidth(1, 20);
        worksheet.SetColumnWidth(2, 15);
        worksheet.SetColumnWidth(3, 25);
        worksheet.SetColumnWidth(4, 20);
        worksheet.SetColumnWidth(5, 30);
        worksheet.SetColumnWidth(6, 30);
        worksheet.SetColumnWidth(7, 25);
        worksheet.SetColumnWidth(8, 30);

        worksheet.Range["A2:H2"].CellStyle.Color = Syncfusion.Drawing.Color.Yellow;
        worksheet.Range["A2:H2"].CellStyle.Font.Bold = true;
        worksheet.Range["A2:H2"].CellStyle.Font.FontName = "Verdana";
        worksheet.Range["A2:H2"].CellStyle.Font.Size = 10;
        worksheet.Range["A2:H2"].CellStyle.WrapText = true;
        worksheet.Range["A2:H2"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
        worksheet.Range["A2:H2"].VerticalAlignment = ExcelVAlign.VAlignCenter;

        worksheet.Range["A2"].Text = "Машины";
        worksheet.Range["B2"].Text = "Модель";
        worksheet.Range["C2"].Text = "Пробег за неделю";
        worksheet.Range["D2"].Text = "1 км/контакт";
        worksheet.Range["E2"].Text = "Число контактов за неделю";
        worksheet.Range["F2"].Text = "Ежемесячная сумма оплаты";
        worksheet.Range["G2"].Text = "Стоимость 1 контакта";
        worksheet.Range["H2"].Text = "CPM (Стоимость 1000 контактов)";

        worksheet.Range["A2:H2"].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
        worksheet.Range["A2:H2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
        worksheet.Range["A2:H2"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
        worksheet.Range["A2:H2"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

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
            worksheet.Range[$"A{row}:H{row}"].CellStyle.WrapText = true;
            worksheet.Range[$"A{row}:H{row}"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
            worksheet.Range[$"A{row}:H{row}"].VerticalAlignment = ExcelVAlign.VAlignCenter;

            worksheet.Range[$"A{row}:H{row}"].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
            worksheet.Range[$"A{row}:H{row}"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
            worksheet.Range[$"A{row}:H{row}"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
            worksheet.Range[$"A{row}:H{row}"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

            if (row % 2 == 1)
            {
                worksheet.Range[$"A{row}:H{row}"].CellStyle.Color = Syncfusion.Drawing.Color.FromArgb(231, 239, 246);
            }

            row++;
        }
    }
}
