using RestWebScrapping.Domain.Models;

namespace RestWebScrapping.Domain.Services;

public interface IOffshoreLeaksService
{
    IEnumerable<OffshoreLeaks> GetEntities();
}