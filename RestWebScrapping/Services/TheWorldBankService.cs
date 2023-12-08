using HtmlAgilityPack;
using RestWebScrapping.Domain.Models;
using RestWebScrapping.Domain.Services;
using RestWebScrapping.Shared;

namespace RestWebScrapping.Services;
using System.Collections.Generic;
public class TheWorldBankService : ITheWorldBankService
{
    public IEnumerable<TheWorldBank> GetEntities()
    {
        // Load page with chrome driver
        const string url = "https://projects.worldbank.org/en/projects-operations/procurement/debarred-firms";
        ChromeDriverManager.NavigateTo(url, 4000);

        var entities = new List<TheWorldBank>();

        // get html code from driver
        var html = ChromeDriverManager.GetHTMLOfByXpath("//table[tbody]");


        // load html code
        var page = new HtmlDocument();
        page.LoadHtml(html);
        var document = page.DocumentNode;

        // get tr children (table row)
        var rows = document.Descendants("tr");
        if (rows == null)
            return entities;

        // map every row
        entities.AddRange(from row in rows
                          select row.ChildNodes // get children of row
        into column
                          where column is { Count: 7 } // validate that is a valid column
                          select new TheWorldBank
                          {
                              // get column value and trim it (remove start and end spaces)
                              FirmName = column[0].InnerText.Trim(),
                              Address = column[2].InnerText.Trim(),
                              Country = column[3].InnerText.Trim(),
                              IneligibilityPeriod = new IneligibilityPeriod
                              {
                                  FromDate = column[4].InnerText.Trim(),
                                  ToDate = column[5].InnerText.Trim()
                              },
                              Grounds = column[6].InnerText.Trim()
                          });


        return entities;
    }
}