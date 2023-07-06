using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeleniumTraining.LayeredStructure.BusinessLogic;

namespace SeleniumTraining.LayeredStructure.PageObjects;

public class WoPage : BasePage
{
    public WoPage(ApplicationContext context) : base(context)
    {
    }
    
    public void Close()
    {
        var woLink = By.XPath("//td[@data-column='WOStatus'][contains(text(), 'New')][1]/following-sibling::td/a");
        var woLinkElement = driver.FindElement(woLink);
        driver.FindElement(By.XPath("//button[@class='close btn-dismiss']")).Click();
        wait.Until(ExpectedConditions.StalenessOf(woLinkElement));
    }

    public string GetActivityLogComment()
    {
        var comment =
            driver.FindElement(
                By.XPath("//*[@data-role='woactivityloggrid']//tbody//tr[1]//td[@data-column='Comment']")).Text;
        return comment;
    }

    public string GetActivityLogActionTitle()
    {
        var action =
            driver.FindElement(
                By.XPath("//*[@data-role='woactivityloggrid']//tbody//tr[1]//td[@data-column='ActionTitle']"));
        return action.Text;
    }

    public void PickUpWo(string pickUpComment)
    {
        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".dialog-level-actions-widget .arrow")))
            .Click();

        driver.FindElement(By.CssSelector("li[data-action=MainFpoQvArea_pickUp] a")).Click();

        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//form[@class='corrigo-form']/div/textarea")))
            .Click();

        driver.FindElement(By.CssSelector("form.corrigo-form textarea")).SendKeys(pickUpComment);
        driver.FindElement(By.CssSelector("div[data-role=woactionpickupeditdialog] button.id-btn-save")).Click();
        var table = driver.FindElement(By.CssSelector("[data-role=woactivityloggrid] tbody"));
        wait.Until(ExpectedConditions.StalenessOf(table));
    }

    
}