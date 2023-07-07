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
        Driver.Navigate()
            .GoToUrl($"{Context.BaseUrl}/corpnet/workorder/workorderlist.aspx");
        Wait.Until(driver => !driver.FindElements(By.XPath("//div[@class='blockUI blockOverlay']")).Any());
    }

    
    public string GetWoStatusFromWoList(string woNumber)
    {
        var woStatus =
            Driver.FindElement(By.XPath(
                    $"//td[@data-column='Number']/a[contains(text(), '{woNumber}')]/../../td[@data-column='WOStatus']"))
                .Text;
        return woStatus;
    }
    
    public string OpenFirstWoFromTheList()
    {
        var woLink = By.XPath("//td[@data-column='WOStatus'][contains(text(), 'New')][1]/following-sibling::td/a");
        var woLinkElement = Driver.FindElement(woLink);
        var woNumber = Driver.FindElement(woLink).Text;

        Driver.FindElement(woLink).Click();

        Wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//*[@data-role='woactivityloggrid']//tbody//tr[1]//td[@data-column='ActionTitle']")));

        return woNumber;
    }
    
    public void ApplyFilters()
    {
        var woLink = By.XPath("//td[@data-column='WOStatus'][contains(text(), 'New')][1]/following-sibling::td/a");
        var woLinkElement = Driver.FindElement(woLink);

        Driver.FindElement(By.CssSelector(".filter-apply")).Click();

        Wait.Until(ExpectedConditions.StalenessOf(woLinkElement));
    }

    public void SetAssigneeFilterToUser()
    {
        Driver.FindElement(By.CssSelector(".id-filter-w_AssigneeType .k-input")).Click();
        Wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@class='k-animation-container']//label[span[text()='- All Types -']]"))).Click();
        Driver.FindElement(By.XPath("//div[@class='k-animation-container']//label[span[contains(text(),'User')]]"))
            .Click();
    }

    public void SetStatusFilterToNew()
    {
        Driver.FindElement(By.CssSelector(".id-filter-w_Status .k-input")).Click();
        Wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@class='k-animation-container']//label[span[text()='- All Statuses -']]"))).Click();
        Driver.FindElement(By.XPath("//div[@class='k-animation-container']//label[span[text()='New']]")).Click();
    }
    
    public void CleanUpWoListFilters()
    {
        while (Driver.FindElements(
                   By.CssSelector("div.filter-block:not([style*='display: none;']) > button.filter-remove")).Count > 0)
            new Actions(Driver)
                .MoveToElement(Driver.FindElement(
                    By.CssSelector("div.filter-block:not([style*='display: none;']) > button.filter-remove"))).Click()
                .Perform();
    }
}