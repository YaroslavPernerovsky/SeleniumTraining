using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTraining;

public class WaitersTests
{
    private IWebDriver drv;

    private long startTime, endTime;
    private WebDriverWait wait;

    [SetUp]
    public void SetUp()
    {
        drv = new ChromeDriver();
        drv.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
        wait = new WebDriverWait(drv, TimeSpan.FromSeconds(10));
        drv.Navigate().GoToUrl("https://google.com");
    }

    [TearDown]
    public void TearDown()
    {
        drv.Quit();
    }

    [Test]
    public void IsElementPresentTest()
    {
        drv.Navigate()
            .GoToUrl($"{Environment.GetEnvironmentVariable("ENT_QA_BASE_URL")}/corpnet/workorder/workorderlist.aspx");

        if (IsElementPresent(By.Id("username")))
        {
            drv.FindElement(By.Id("username"))
                .SendKeys(Environment.GetEnvironmentVariable("ENT_QA_USER"));
            drv.FindElement(By.Id("password")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_PASS"));
            drv.FindElement(By.Name("_companyText")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_COMPANY"));
            drv.FindElement(By.CssSelector("input.btn.login-submit-button")).Click();
        }

        if (wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("username"))))
        {
            drv.FindElement(By.CssSelector(".menu-secondary .menu-user")).Click();
            drv.FindElement(By.CssSelector("li.menu-logout a")).Click();
        }
    }


    private bool IsElementPresent(By locator)
    {
        return drv.FindElements(locator).Count > 0;
    }


    [Test]
    public void WaitersTest()
    {
        startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        try
        {
            wait.Until(drv => drv.FindElement(By.Name("none")));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            endTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            Console.WriteLine("WaitTime: " + (endTime - startTime) / 1000 + " s");
        }
    }
}