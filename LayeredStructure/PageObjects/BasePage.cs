using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTraining.LayeredStructure.BusinessLogic;

namespace SeleniumTraining.LayeredStructure.PageObjects;

public abstract class BasePage
{
    protected ApplicationContext context;
    protected readonly IWebDriver driver;
    protected WebDriverWait wait;

    protected BasePage(ApplicationContext context)
    {
        this.context = context;
        driver = context.driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }
    
}