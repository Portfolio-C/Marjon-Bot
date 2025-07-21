namespace MarjonBot.Domain.Entities;

public class Report
{
    public required long UserId { get; set; }
    public int Id { get; set; }
    public required string CarNumber { get; set; }
    public required string CarModel { get; set; }
    public int MilleageForTheWeek { get; set; }
    public int ContactPerKm { get; set; }
    public int NumberOfContactsPerWeek { get; set; }
    public double MonthlyPaymentAmount { get; set; }
    public decimal CostOfOneContact { get; set; }
}
