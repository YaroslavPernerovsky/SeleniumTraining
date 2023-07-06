using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeleniumTraining.LayeredStructure.BusinessLogic;

namespace SeleniumTraining.LayeredStructure.PageObjects;

public class LoginPage : BasePage
{
    private By usernameFld = By.Name("username");
    private By passwordFld = By.Name("password");
    private By companyFld = By.Name("_companyText");
    private By loginBtn = By.CssSelector(".login-submit-button");
    private By menuEl = By.CssSelector("div.menu-secondary ul li.menu-user a.menu-drop");
    
    public LoginPage(ApplicationContext context) : base(context)
    {
    }
    
    public void LoginWithDefaultUser()
    {
        driver.Navigate().GoToUrl( context.baseUrl + "/corpnet/Login.aspx");
        driver.FindElement(usernameFld).SendKeys(context.username);
        driver.FindElement(passwordFld).SendKeys(context.password);
        driver.FindElement(companyFld).SendKeys(context.company);
        driver.FindElement(loginBtn).Click();
        wait.Until(
            ExpectedConditions.ElementIsVisible(menuEl));
    }
}