using OpenQA.Selenium;

namespace SeleniumTraining.LayeredStructure.BusinessLogic;

public record ApplicationContext
{
    public IWebDriver Driver { get; set;} = null!;
    public string BaseUrl { get; set;} = null!;
    public string Username { get; set;} = null!;
    public string Password { get; set;} = null!;
    public string Company { get; set;} = null!;
}