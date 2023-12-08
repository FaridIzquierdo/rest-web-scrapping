using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RestWebScrapping.Domain.Models;
using RestWebScrapping.Domain.Services;
using RestWebScrapping.Shared;
using static System.Net.WebUtility;
namespace RestWebScrapping.Services;

public class OfacService : IOfacService
{
    public IEnumerable<OFAC> GetEntities()
    {
        // Load page with chrome driver
        //agregar los catchs para ver errores
        //pasarlo como archivo 
        const string url = "https://sanctionssearch.ofac.treas.gov/";
        const string programValue = "BALKANS";
        ChromeDriverManager.NavigateTo(url);
        var entities = new List<OFAC>();

        //enum asignarles a los by
        //acerca sobre la libreria  ChromeDriverManager
        // get the dropdown for program select options
        var selectElement = ChromeDriverManager.GetElementBy(By.Id("ctl00_MainContent_lstPrograms"));
        if (selectElement == null)
            return entities;

        // deselect current option and select the new value
        var select = new SelectElement(selectElement);
        select.DeselectAll();
        select.SelectByValue(programValue);


        // get search input for sends request
        var searchElement = ChromeDriverManager.GetElementBy(By.Id("ctl00_MainContent_btnSearch"));
        if (searchElement == null)
            return entities;

        // send search request
        searchElement.Click();

        // get html code
        var html = ChromeDriverManager.GetHTMLOfBy(By.Id("gvSearchResults"));

        // lad html code
        var page = new HtmlDocument();
        page.LoadHtml(html);

        // get doc code
        var document = page.DocumentNode;
        if (document == null)
            return entities;

        // get rows from table search result
        var rows = document.Descendants("tr");

        // map every row
        entities.AddRange(from row in rows
                              // get columns for every row
                          select row.Descendants("td").ToList() into column
                          where column is { Count: 6 }
                          select new OFAC
                          {
                              // assign columns values
                              Name = column[0].InnerText.Trim(),
                              // transform html code symbol to simple string
                              Address = HtmlDecode(column[1].InnerText).Trim(),
                              Type = column[2].InnerText.Trim(),
                              Program = column[3].InnerText.Trim(),
                              List = column[4].InnerText.Trim(),
                              Score = HtmlDecode(column[5].InnerText).Trim()
                          }
        );

        return entities;
    }
}