using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using SeleniumTraining.LayeredStructure.BusinessLogic;

namespace SeleniumTraining.LayeredStructure.PageObjects;

public class WoListPage : BasePage
{
    public WoListPage(ApplicationContext context) : base(context)
    { }
    
    public void Open()
    {
        driver.Navigate()
            .GoToUrl($"{context.baseUrl}/corpnet/workorder/workorderlist.aspx");
        wait.Until(driver => !driver.FindElements(By.XPath("//div[@class='blockUI blockOverlay']")).Any());
    }

    
    public string GetWoStatusFromWoList(string woNumber)
    {
        var woStatus =
            driver.FindElement(By.XPath(
                    $"//td[@data-column='Number']/a[contains(text(), '{woNumber}')]/../../td[@data-column='WOStatus']"))
                .Text;
        return woStatus;
    }
    
    public string OpenFirstWoFromTheList()
    {
        var woLink = By.XPath("//td[@data-column='WOStatus'][contains(text(), 'New')][1]/following-sibling::td/a");
        var woLinkElement = driver.FindElement(woLink);
        var woNumber = driver.FindElement(woLink).Text;

        driver.FindElement(woLink).Click();

        wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//*[@data-role='woactivityloggrid']//tbody//tr[1]//td[@data-column='ActionTitle']")));

        return woNumber;
    }
    
    public void ApplyFilters()
    {
        var woLink = By.XPath("//td[@data-column='WOStatus'][contains(text(), 'New')][1]/following-sibling::td/a");
        var woLinkElement = driver.FindElement(woLink);

        driver.FindElement(By.CssSelector(".filter-apply")).Click();

        wait.Until(ExpectedConditions.StalenessOf(woLinkElement));
    }

    public void SetAssigneeFilterToUser()
    {
        driver.FindElement(By.CssSelector(".id-filter-w_AssigneeType .k-input")).Click();
        wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@class='k-animation-container']//label[span[text()='- All Types -']]"))).Click();
        driver.FindElement(By.XPath("//div[@class='k-animation-container']//label[span[contains(text(),'User')]]"))
            .Click();
    }

    public void SetStatusFilterToNew()
    {
        driver.FindElement(By.CssSelector(".id-filter-w_Status .k-input")).Click();
        wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@class='k-animation-container']//label[span[text()='- All Statuses -']]"))).Click();
        driver.FindElement(By.XPath("//div[@class='k-animation-container']//label[span[text()='New']]")).Click();
    }
    
    public void CleanUpWoListFilters()
    {
        while (driver.FindElements(
                   By.CssSelector("div.filter-block:not([style*='display: none;']) > button.filter-remove")).Count > 0)
            new Actions(driver)
                .MoveToElement(driver.FindElement(
                    By.CssSelector("div.filter-block:not([style*='display: none;']) > button.filter-remove"))).Click()
                .Perform();
    }
}