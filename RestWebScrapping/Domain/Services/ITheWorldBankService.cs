using RestWebScrapping.Domain.Models;

namespace RestWebScrapping.Domain.Services;

public interface ITheWorldBankService
{
    IEnumerable<TheWorldBank> GetEntities();
}