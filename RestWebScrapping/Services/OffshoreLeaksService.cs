using HtmlAgilityPack;
using RestWebScrapping.Domain.Models;
using RestWebScrapping.Domain.Services;

namespace RestWebScrapping.Services;

public class OffshoreLeaksService : IOffshoreLeaksService
{
    public IEnumerable<OffshoreLeaks> GetEntities()
    {
        // load static web page
        const string url = "https://offshoreleaks.icij.org/investigations/pandora-papers";
        var entities = new List<OffshoreLeaks>();
        var page = new HtmlWeb();
        var document = page.Load(url).DocumentNode;

        if (document == null)
            return entities;

        // get data from div element (contains data from value)
        var dataFrom = document.SelectSingleNode("//div[contains(@class, 'source-header__container__label')]");
        if (dataFrom == null)
            return entities;

        // get table element
        var table = document.SelectSingleNode("//table[tbody]");
        if (table == null)
            return entities;

        // get rows from table
        var rows = document.Descendants("tr");
        if (rows == null)
            return entities;

        // map every row
        entities.AddRange(from row in rows
                              // get columns from row
                          select row.Descendants("td").ToList() into column
                          where column.Count == 3 // check valid column
                          select new OffshoreLeaks
                          {
                              // map columns value
                              Entity = column[0].InnerText.Trim(),
                              Jurisdiction = column[1].InnerText.Trim(),
                              LinkedTo = column[2].InnerText.Trim(),
                              DataFrom = dataFrom.InnerText.Trim()
                          });

        return entities;
    }
}