using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTraining;

[TestFixture]
public class LocatorsStrategies
{
    [OneTimeSetUp]
    public void Setup()
    {
        drv = new ChromeDriver();
        drv.Navigate().GoToUrl("file://" + Directory.GetCurrentDirectory() + "/Examples.html");
    }

    [OneTimeTearDown]
    public void ShutDown()
    {
        drv.Quit();
    }

    private IWebDriver drv;


    [Test]
    public void RelativeSearchTests()
    {
        var form1 = drv.FindElement(By.Id("first"));
        FillDefaultValues(form1);

        var form2 = drv.FindElement(By.Id("second"));
        FillDefaultValues(form2);
    }

    [Test]
    public void FindElementsTableTest()
    {
        var table = drv.FindElement(By.Id("table"));
        var rows = table.FindElements(By.CssSelector("tbody>tr"));

        foreach (var row in rows)
        {
            var name = row.FindElement(By.XPath(".//td[1]")).Text;
            var mail = row.FindElement(By.XPath(".//td[2]")).Text;

            Console.WriteLine("--------------------");
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Mail: " + mail);
        }

        foreach (var row in rows)
        {
            var col = row.FindElements(By.TagName("td"));
            var name = col[0].Text;
            var addr = col[2].Text;

            Console.WriteLine("++++++++++++++++++++");
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Address: " + addr);
        }
    }

    [Test]
    public void FindElementsExceptionTest()
    {
        Console.WriteLine(IsElementPresent(By.XPath("//*[@name='login']"))
            ? "Element is present"
            : "Element is missed");
    }


    [Test]
    public void FindElementWithJs()
    {
        var jsExecutor = (IJavaScriptExecutor)drv;

        var links = (ReadOnlyCollection<IWebElement>)jsExecutor.ExecuteScript(
            " return document.getElementsByClassName('username')");

        foreach (var link in links) Console.WriteLine("Address: " + link.Text);
    }


    private void FillDefaultValues(IWebElement form)
    {
        form.FindElement(By.Name("login")).SendKeys("This is login");
        form.FindElement(By.Name("password")).SendKeys("This is password");
        form.FindElement(By.Name("company")).SendKeys("This is company");
    }


    // private bool isElementPresent(By locator)
    // {
    //     try
    //     {
    //         drv.FindElement(locator);
    //         return true;
    //     }
    //     catch (NoSuchElementException ex)
    //     {
    //         return false;
    //     }
    // }

    private bool IsElementPresent(By locator)
    {
        return drv.FindElements(locator).Count > 0;
    }
}