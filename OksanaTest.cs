using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTraining;

public class OksanaTest
{
    private IWebDriver drv;
    private WebDriverWait wait;

    [SetUp]
    public void Setup()
    {
        drv = new ChromeDriver();

        wait = new WebDriverWait(drv, TimeSpan.FromSeconds(5));

        drv.Navigate().GoToUrl($"{Environment.GetEnvironmentVariable("ENT_QA_BASE_URL")}/CorpNet/Login.aspx");

        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("username")))
            .SendKeys(Environment.GetEnvironmentVariable("ENT_QA_USER"));

        drv.FindElement(By.Id("password")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_PASS"));
        drv.FindElement(By.Name("_companyText")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_COMPANY"));
        drv.FindElement(By.CssSelector("input.btn.login-submit-button")).Click();

        wait.Until(
            ExpectedConditions.ElementIsVisible(By.CssSelector("div.menu-secondary ul li.menu-user a.menu-drop")));
    }

    [TearDown]
    public void TearDown()
    {
        drv.Quit();
    }


    [Test]
    public void TestLogin()
    {
        Assert.True(IsVisible(By.CssSelector("div.menu-secondary ul li.menu-user a.menu-drop")));
    }

    [Test]
    public void TestWOflow()
    {
        var pickUpComment = "Pick-Up Comment From Autotest";
        drv.Navigate()
            .GoToUrl($"{Environment.GetEnvironmentVariable("ENT_QA_BASE_URL")}/corpnet/workorder/workorderlist.aspx");


        var woLink = By.XPath("//td[@data-column='WOStatus'][contains(text(), 'New')][1]/following-sibling::td/a");

        var woLinkElement = wait.Until(drv => drv.FindElement(woLink));

        var woNumber = drv.FindElement(woLink).Text;

        drv.FindElement(woLink).Click();
        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".dialog-level-actions-widget .arrow")))
            .Click();

        drv.FindElement(By.CssSelector("li[data-action=MainFpoQvArea_pickUp] a")).Click();
        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//form[@class='corrigo-form']/div/textarea")))
            .Click();

        drv.FindElement(By.CssSelector("form.corrigo-form textarea")).SendKeys(pickUpComment);
        drv.FindElement(By.CssSelector("div[data-role=woactionpickupeditdialog] button.id-btn-save")).Click();
        var table = drv.FindElement(By.CssSelector("[data-role=woactivityloggrid] tbody"));
        wait.Until(ExpectedConditions.StalenessOf(table));

        var action =
            drv.FindElement(
                By.XPath("//*[@data-role='woactivityloggrid']//tbody//tr[1]//td[@data-column='ActionTitle']"));
        Assert.That(action.Text, Is.EqualTo("Picked Up"));

        var comment =
            drv.FindElement(By.XPath("//*[@data-role='woactivityloggrid']//tbody//tr[1]//td[@data-column='Comment']"));
        Assert.That(comment.Text, Is.EqualTo(pickUpComment));

        drv.FindElement(By.XPath("//button[@class='close btn-dismiss']")).Click();
        wait.Until(ExpectedConditions.StalenessOf(woLinkElement));

        var woStatus =
            drv.FindElement(By.XPath(
                $"//td[@data-column='Number']/a[contains(text(), '{woNumber}')]/../../td[@data-column='WOStatus']"));
        Assert.That(woStatus.Text, Is.EqualTo("Open"));
    }

    private bool IsVisible(By locator)
    {
        return drv.FindElements(locator).Count > 0;
    }
}