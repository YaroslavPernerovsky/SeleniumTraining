using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTraining;

[TestFixture]
public class ImmutableInteractions : BaseTest
{
    [SetUp]
    public void OpenDemoHtml()
    {
        drv.Navigate().GoToUrl("file://" + Path.Combine(Environment.CurrentDirectory, @"Resources\", "Examples.html"));
    }

    [Test]
    public void GetAttributeTest()
    {
        var textInput = drv.FindElement(By.Id("text"));

        Console.WriteLine("GetAttr: " + textInput.GetAttribute("myAttribute"));
        Console.WriteLine("GetAttr: " + textInput.GetAttribute("namespaceURI"));

        textInput.SendKeys(Keys.Enter);

        var link = drv.FindElement(By.Id("link"));
        var select = new SelectElement(drv.FindElement(By.Name("currency_code")));


        textInput.SendKeys("Test Message");
        Console.WriteLine("GetText: " + textInput.Text);
        Console.WriteLine("GetAttr: " + textInput.GetAttribute("value"));

        Console.WriteLine("GetLink: " + link.GetAttribute("href"));

        select.SelectByIndex(0);
        Console.WriteLine("Select: " + select.SelectedOption.GetAttribute("selected"));

        foreach (var selected in select.AllSelectedOptions)
            Console.WriteLine("Selected: " +
                              selected.Text + " : " +
                              selected.GetAttribute("selected"));
    }

    [Test]
    public void GetTextTest()
    {
        var invisible = drv.FindElement(By.Id("invisible"));
        var spaces = drv.FindElement(By.Id("spaces"));
        var spacespre = drv.FindElement(By.Id("spacespre"));
        var div = drv.FindElement(By.Id("div"));

        Console.WriteLine("GetInvisibleText: " + invisible.Text);
        Console.WriteLine("GetInvisibleAttr: " + invisible.GetAttribute("textContent"));

        Console.WriteLine("GetSpacesText: " + spaces.Text);
        Console.WriteLine("GetSpacesAttr: " + spaces.GetAttribute("textContent"));

        Console.WriteLine("GetSpacesPreText: " + spacespre.Text);
        Console.WriteLine("GetSpacesPreAttr: " + spacespre.GetAttribute("textContent"));

        Console.WriteLine("GetTextForNestedElements: " + div.Text);
        Console.WriteLine("GetAttrForNestedElements: " + div.GetAttribute("textContent"));
    }

    [Test]
    public void IsDisplayedTest()
    {
        drv.Navigate().GoToUrl("https://output.jsbin.com/saqoca/2");
        Console.WriteLine("transparent: " + drv.FindElement(By.Id("transparent")).Displayed);
        Console.WriteLine("white: " + drv.FindElement(By.Id("white")).Displayed);
        Console.WriteLine("behind: " + drv.FindElement(By.Id("behind")).Displayed);
        Console.WriteLine("outside: " + drv.FindElement(By.Id("outside")).Displayed);
        Console.WriteLine("shifted: " + drv.FindElement(By.Id("shifted")).Displayed);
    }
}