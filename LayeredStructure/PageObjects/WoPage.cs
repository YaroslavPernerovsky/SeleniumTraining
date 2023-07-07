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
        var woLinkElement = Driver.FindElement(woLink);
        Driver.FindElement(By.XPath("//button[@class='close btn-dismiss']")).Click();
        Wait.Until(ExpectedConditions.StalenessOf(woLinkElement));
    }

    public string GetActivityLogComment()
    {
        var comment =
            Driver.FindElement(
                By.XPath("//*[@data-role='woactivityloggrid']//tbody//tr[1]//td[@data-column='Comment']")).Text;
        return comment;
    }

    public string GetActivityLogActionTitle()
    {
        var action =
            Driver.FindElement(
                By.XPath("//*[@data-role='woactivityloggrid']//tbody//tr[1]//td[@data-column='ActionTitle']"));
        return action.Text;
    }

    public void PickUpWo(string pickUpComment)
    {
        Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".dialog-level-actions-widget .arrow")))
            .Click();

        Driver.FindElement(By.CssSelector("li[data-action=MainFpoQvArea_pickUp] a")).Click();

        Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//form[@class='corrigo-form']/div/textarea")))
            .Click();

        Driver.FindElement(By.CssSelector("form.corrigo-form textarea")).SendKeys(pickUpComment);
        Driver.FindElement(By.CssSelector("div[data-role=woactionpickupeditdialog] button.id-btn-save")).Click();
        var table = Driver.FindElement(By.CssSelector("[data-role=woactivityloggrid] tbody"));
        Wait.Until(ExpectedConditions.StalenessOf(table));
    }

    
}