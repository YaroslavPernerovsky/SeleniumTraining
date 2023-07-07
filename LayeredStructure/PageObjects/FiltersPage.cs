using OpenQA.Selenium;
using SeleniumTraining.LayeredStructure.BusinessLogic;

namespace SeleniumTraining.LayeredStructure.PageObjects;

public class FiltersPage : BasePage
{
    public FiltersPage(ApplicationContext context) : base(context)
    {
    }

    public void Open()
    {
        Driver.FindElement(By.CssSelector(".plain-actions .icon-filter")).Click();
    }

    public void Close()
    {
        Driver.FindElement(By.CssSelector(".dialog-form-buttons button.btn-primary")).Click();
    }

    public void SetNewFiltersToStatusAndAssignee()
    {
        
        Wait.Until(driver => !driver.FindElements(By.XPath("//div[@class='blockUI blockOverlay']")).Any());
        Driver.FindElement(By.CssSelector("[columnid='w_Status']")).Click();
        Driver.FindElement(By.CssSelector("[columnid='w_AssigneeType']")).Click();
      
    }
}