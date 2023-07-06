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
        driver.FindElement(By.CssSelector(".plain-actions .icon-filter")).Click();
    }

    public void Close()
    {
        driver.FindElement(By.CssSelector(".dialog-form-buttons button.btn-primary")).Click();
    }

    public void SetNewFiltersToStatusAndAssignee()
    {
        
        wait.Until(driver => !driver.FindElements(By.XPath("//div[@class='blockUI blockOverlay']")).Any());
        driver.FindElement(By.CssSelector("[columnid='w_Status']")).Click();
        driver.FindElement(By.CssSelector("[columnid='w_AssigneeType']")).Click();
      
    }
}