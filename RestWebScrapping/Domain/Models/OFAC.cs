namespace RestWebScrapping.Domain.Models;

public class OFAC
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Program { get; set; } = null!;
    public string List { get; set; } = null!;
    public string Score { get; set; } = null!;
}