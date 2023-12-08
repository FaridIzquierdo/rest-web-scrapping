namespace RestWebScrapping.Domain.Models;

public class TheWorldBank
{
    public string FirmName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Country { get; set; } = null!;
    public IneligibilityPeriod IneligibilityPeriod { get; set; } = null!;
    public string Grounds { get; set; } = null!;
}

public class IneligibilityPeriod
{
    public string FromDate { get; set; } = null!;
    public string ToDate { get; set; } = null!;
}