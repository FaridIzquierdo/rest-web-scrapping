using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace RestWebScrapping.Shared;

public static class ChromeDriverManager
{
    private static ChromeDriver? _driver;

    private static void _InitDriver()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless");
        _driver = new ChromeDriver(options);
    }

    public static void NavigateTo(string url, int wait = 5000)
    {
        _InitDriver();

        _driver?.Navigate().GoToUrl(url);
        Thread.Sleep(wait);
    }

    public static string? GetHTMLOfBy(By by)
    {
        var element = _driver?.FindElement(by);
        return element?.GetAttribute("innerHTML");
    }

    public static IWebElement? GetElementBy(By by)
    {
        return _driver?.FindElement(by);
    }

    public static string? GetHTMLOfByXpath(string xpath)
    {
        return GetHTMLOfBy(By.XPath(xpath));
    }

    public static void Quit()
    {
        _driver?.Quit();
    }
}