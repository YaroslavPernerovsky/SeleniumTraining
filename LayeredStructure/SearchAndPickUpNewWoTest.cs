using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTraining.LayeredStructure;

[TestFixture]
public class SearchAndPickUpNewWoTest
{
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var options = new ChromeOptions();
        options.AddArguments(
            "start-maximized"
        );

        driver = new ChromeDriver(options);
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); // Explicit Wait

        driver.Navigate().GoToUrl(Environment.GetEnvironmentVariable("ENT_QA_BASE_URL") + "/corpnet/Login.aspx");
        driver.FindElement(By.Name("username")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_USER"));
        driver.FindElement(By.Name("password")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_PASS"));
        driver.FindElement(By.Name("_companyText")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_COMPANY"));
        driver.FindElement(By.CssSelector(".login-submit-button")).Click();
        wait.Until(
            ExpectedConditions.ElementIsVisible(By.CssSelector("div.menu-secondary ul li.menu-user a.menu-drop")));
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        driver.Quit();
    }

    private IWebDriver driver;
    private WebDriverWait wait;

    [Test]
    public void SearchAndPickupWO()
    {
        const string pickUpComment = "Autotest";

        driver.Navigate()
            .GoToUrl($"{Environment.GetEnvironmentVariable("ENT_QA_BASE_URL")}/corpnet/workorder/workorderlist.aspx");

        wait.Until(driver => !driver.FindElements(By.XPath("//div[@class='blockUI blockOverlay']")).Any());

        while (driver.FindElements(
                   By.CssSelector("div.filter-block:not([style*='display: none;']) > button.filter-remove")).Count > 0)
            new Actions(driver)
                .MoveToElement(driver.FindElement(
                    By.CssSelector("div.filter-block:not([style*='display: none;']) > button.filter-remove"))).Click()
                .Perform();

        driver.FindElement(By.CssSelector(".plain-actions .icon-filter")).Click();
        wait.Until(driver => !driver.FindElements(By.XPath("//div[@class='blockUI blockOverlay']")).Any());
        driver.FindElement(By.CssSelector("[columnid='w_Status']")).Click();
        driver.FindElement(By.CssSelector("[columnid='w_AssigneeType']")).Click();
        driver.FindElement(By.CssSelector(".dialog-form-buttons button.btn-primary")).Click();
        driver.FindElement(By.CssSelector(".id-filter-w_Status .k-input")).Click();
        wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@class='k-animation-container']//label[span[text()='- All Statuses -']]"))).Click();
        driver.FindElement(By.XPath("//div[@class='k-animation-container']//label[span[text()='New']]")).Click();
        driver.FindElement(By.CssSelector(".id-filter-w_AssigneeType .k-input")).Click();
        wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@class='k-animation-container']//label[span[text()='- All Types -']]"))).Click();
        driver.FindElement(By.XPath("//div[@class='k-animation-container']//label[span[contains(text(),'User')]]"))
            .Click();

        var woLink = By.XPath("//td[@data-column='WOStatus'][contains(text(), 'New')][1]/following-sibling::td/a");
        var woLinkElement = driver.FindElement(woLink);

        driver.FindElement(By.CssSelector(".filter-apply")).Click();

        wait.Until(ExpectedConditions.StalenessOf(woLinkElement));

        woLinkElement = driver.FindElement(woLink);

        var woNumber = driver.FindElement(woLink).Text;

        driver.FindElement(woLink).Click();

        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".dialog-level-actions-widget .arrow")))
            .Click();

        driver.FindElement(By.CssSelector("li[data-action=MainFpoQvArea_pickUp] a")).Click();
        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//form[@class='corrigo-form']/div/textarea")))
            .Click();

        driver.FindElement(By.CssSelector("form.corrigo-form textarea")).SendKeys(pickUpComment);
        driver.FindElement(By.CssSelector("div[data-role=woactionpickupeditdialog] button.id-btn-save")).Click();
        var table = driver.FindElement(By.CssSelector("[data-role=woactivityloggrid] tbody"));
        wait.Until(ExpectedConditions.StalenessOf(table));

        var action =
            driver.FindElement(
                By.XPath("//*[@data-role='woactivityloggrid']//tbody//tr[1]//td[@data-column='ActionTitle']"));
        Assert.That(action.Text, Is.EqualTo("Picked Up"));

        var comment =
            driver.FindElement(
                By.XPath("//*[@data-role='woactivityloggrid']//tbody//tr[1]//td[@data-column='Comment']"));
        Assert.That(comment.Text, Is.EqualTo(pickUpComment));

        driver.FindElement(By.XPath("//button[@class='close btn-dismiss']")).Click();
        wait.Until(ExpectedConditions.StalenessOf(woLinkElement));

        var woStatus =
            driver.FindElement(By.XPath(
                $"//td[@data-column='Number']/a[contains(text(), '{woNumber}')]/../../td[@data-column='WOStatus']"));
        Assert.That(woStatus.Text, Is.EqualTo("Open"));
    }
}