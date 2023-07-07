using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTraining.LayeredStructure.BusinessLogic;

namespace SeleniumTraining.LayeredStructure.PageObjects;

public abstract class BasePage
{
    protected readonly ApplicationContext Context;
    protected readonly IWebDriver Driver;
    protected readonly WebDriverWait Wait;

    protected BasePage(ApplicationContext context)
    {
        this.Context = context;
        Driver = context.Driver;
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
    }
    
}