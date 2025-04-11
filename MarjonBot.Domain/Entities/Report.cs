namespace MarjonBot.Domain.Entities;

public class Report
{
    public int Id { get; set; }
    public string CarNumber { get; set; }
    public string CarModel { get; set; }
    public int MilleageForTheWeek { get; set; }
    public int ContactPerKm { get; set; }
    public int NumberOfContactsPerWeek { get; set; }
    public int MonthlyPaymentAmount { get; set; }
    public int CPM { get; set; }
}
