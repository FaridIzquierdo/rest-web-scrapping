using RestWebScrapping.Domain.Models;

namespace RestWebScrapping.Domain.Services;

public interface IOfacService
{
    IEnumerable<OFAC> GetEntities();
}