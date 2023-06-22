using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTraining;

[TestFixture]
public class BaseTest
{
    [OneTimeSetUp]
    public void Setup()
    {
        var opt = new ChromeOptions();
        opt.UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore;
        drv = new ChromeDriver(opt);
        wait = new WebDriverWait(drv, TimeSpan.FromSeconds(10));
    }

    [OneTimeTearDown]
    public void ShutDown()
    {
        drv.Quit();
    }

    protected IWebDriver drv;
    protected WebDriverWait wait;

    protected internal static string DemoHtmlUrl()
    {
        return "file://" + Path.Combine(Environment.CurrentDirectory, @"Resources\", "Examples.html");
    }
}