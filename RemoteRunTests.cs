using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTraining;

public class RemoteRunTests
{
    private IWebDriver drv;
    private WebDriverWait wait;

    [SetUp]
    public void ConnectToBrowser()
    {
        var opt = new ChromeOptions();


        drv = new RemoteWebDriver(new Uri("http://localhost:4444"), opt);

        wait = new WebDriverWait(drv, TimeSpan.FromSeconds(5));
    }

    [TearDown]
    public void CleanUp()
    {
        drv.Quit();
    }

    [Test]
    public void RemoteTest()
    {
        drv.Navigate().GoToUrl($"{Environment.GetEnvironmentVariable("ENT_QA_BASE_URL")}/CorpNet/Login.aspx");

        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("username")))
            .SendKeys(Environment.GetEnvironmentVariable("ENT_QA_USER"));

        drv.FindElement(By.Id("password")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_PASS"));
        drv.FindElement(By.Name("_companyText")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_COMPANY"));
        drv.FindElement(By.CssSelector("input.btn.login-submit-button")).Click();

        wait.Until(
            ExpectedConditions.ElementIsVisible(By.CssSelector("div.menu-secondary ul li.menu-user a.menu-drop")));
    }
}