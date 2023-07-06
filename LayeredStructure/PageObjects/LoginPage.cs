using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using SeleniumTraining.LayeredStructure.BusinessLogic;

namespace SeleniumTraining.LayeredStructure.PageObjects;

public class LoginPage : BasePage
{
    [FindsBy(How = How.Name, Using = "_companyText")]
    private readonly IWebElement companyFld;

    [FindsBy(How = How.CssSelector, Using = ".login-submit-button")]
    private readonly IWebElement loginBtn;

    [FindsBy(How = How.CssSelector, Using = "div.menu-secondary ul li.menu-user a.menu-drop")]
    private readonly IWebElement menuEl;

    [FindsBy(How = How.Name, Using = "password")]
    private readonly IWebElement passwordFld;

    [FindsBy(How = How.Name, Using = "username")]
    private IWebElement usernameFld;

    public LoginPage(ApplicationContext context) : base(context)
    {
        PageFactory.InitElements(driver, this);
    }

    public void LoginWithDefaultUser()
    {
        driver.Navigate().GoToUrl(context.baseUrl + "/corpnet/Login.aspx");
        usernameFld.SendKeys(context.username);
        passwordFld.SendKeys(context.password);
        companyFld.SendKeys(context.company);
        loginBtn.Click();
        wait.Until(
            ExpectedConditions.ElementToBeClickable(menuEl));
    }
}