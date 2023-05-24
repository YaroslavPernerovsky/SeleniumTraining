using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace SeleniumTraining;

[TestFixture]
public class CssValuesTest
{
    [SetUp]
    public void StartBrowsers()
    {
        ff = new FirefoxDriver();
        cr = new ChromeDriver();
        sf = new SafariDriver();

        ff.Navigate().GoToUrl(DemoHtmlUrl());
        cr.Navigate().GoToUrl(DemoHtmlUrl());
        sf.Navigate().GoToUrl(DemoHtmlUrl());
    }

    [TearDown]
    public void StopBrowsers()
    {
        ff.Quit();
        cr.Quit();
        sf.Quit();
    }

    private IWebDriver ff, cr, sf;

    [Test]
    public void GetCssValues()
    {
        var text = By.CssSelector("p.css");

        var fftext = ff.FindElement(text);
        var sftext = sf.FindElement(text);
        var crtext = cr.FindElement(text);

        Console.WriteLine("FF color: " + fftext.GetCssValue("color"));
        Console.WriteLine("CR color: " + crtext.GetCssValue("color"));
        Console.WriteLine("SF color: " + sftext.GetCssValue("color"));

        Console.WriteLine("FF bk_color: " + fftext.GetCssValue("background-color"));
        Console.WriteLine("CR bk_color: " + crtext.GetCssValue("background-color"));
        Console.WriteLine("SF bk_color: " + sftext.GetCssValue("background-color"));

        Console.WriteLine("FF font: " + fftext.GetCssValue("font"));
        Console.WriteLine("CR font: " + crtext.GetCssValue("font"));
        Console.WriteLine("SF font: " + sftext.GetCssValue("font"));

        Console.WriteLine("FF font-family: " + fftext.GetCssValue("font-family"));
        Console.WriteLine("CR font-family: " + crtext.GetCssValue("font-family"));
        Console.WriteLine("SF font-family: " + sftext.GetCssValue("font-family"));

        Console.WriteLine("FF font-weight: " + fftext.GetCssValue("font-weight"));
        Console.WriteLine("CR font-weight: " + crtext.GetCssValue("font-weight"));
        Console.WriteLine("SF font-weight: " + sftext.GetCssValue("font-weight"));
    }

    private string DemoHtmlUrl()
    {
        return "file://" + Path.Combine(Environment.CurrentDirectory, @"Resources\", "Examples.html");
    }
}