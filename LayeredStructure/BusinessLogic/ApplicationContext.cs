using OpenQA.Selenium;

namespace SeleniumTraining.LayeredStructure.BusinessLogic;

public record ApplicationContext
{
    public IWebDriver driver { get; set;}
    public string baseUrl { get; set;}
    public string username { get; set;}
    public string password { get; set;}
    public string company { get; set;}

}