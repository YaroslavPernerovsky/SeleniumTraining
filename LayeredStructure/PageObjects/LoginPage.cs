using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeleniumTraining.LayeredStructure.BusinessLogic;

namespace SeleniumTraining.LayeredStructure.PageObjects;

public class LoginPage : BasePage
{
    public LoginPage(ApplicationContext context) : base(context)
    {
    }
    
    public void LoginWithDefaultUser()
    {
        driver.Navigate().GoToUrl( context.baseUrl + "/corpnet/Login.aspx");
        driver.FindElement(By.Name("username")).SendKeys(context.username);
        driver.FindElement(By.Name("password")).SendKeys(context.password);
        driver.FindElement(By.Name("_companyText")).SendKeys(context.company);
        driver.FindElement(By.CssSelector(".login-submit-button")).Click();
        wait.Until(
            ExpectedConditions.ElementIsVisible(By.CssSelector("div.menu-secondary ul li.menu-user a.menu-drop")));
    }
}