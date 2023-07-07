using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using SeleniumTraining.LayeredStructure.BusinessLogic;

namespace SeleniumTraining.LayeredStructure.PageObjects;

public class LoginPage : BasePage
{
    [FindsBy(How = How.Name, Using = "_companyText")]
    private readonly IWebElement _companyFld;

    [FindsBy(How = How.CssSelector, Using = ".login-submit-button")]
    private readonly IWebElement _loginBtn;

    [FindsBy(How = How.CssSelector, Using = "div.menu-secondary ul li.menu-user a.menu-drop")]
    private readonly IWebElement _menuEl;

    [FindsBy(How = How.Name, Using = "password")]
    private readonly IWebElement _passwordFld;

    [FindsBy(How = How.Name, Using = "username")]
    private IWebElement _usernameFld;

    public LoginPage(ApplicationContext context) : base(context)
    {
        PageFactory.InitElements(Driver, this);
    }

    public void LoginWithDefaultUser()
    {
        Driver.Navigate().GoToUrl(Context.BaseUrl + "/corpnet/Login.aspx");
        _usernameFld.SendKeys(Context.Username);
        _passwordFld.SendKeys(Context.Password);
        _companyFld.SendKeys(Context.Company);
        _loginBtn.Click();
        Wait.Until(
            ExpectedConditions.ElementToBeClickable(_menuEl));
    }
}