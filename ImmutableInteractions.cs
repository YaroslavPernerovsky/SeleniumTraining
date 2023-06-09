using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTraining;

[TestFixture]
public class ImmutableInteractions : BaseTest
{
    [SetUp]
    public void OpenDemoHtml()
    {
        drv.Navigate().GoToUrl(DemoHtmlUrl());
    }

    [Test]
    public void GetAttributeTest()
    {
        var textInput = drv.FindElement(By.Id("text"));

        Console.WriteLine("GetAttr: " + textInput.GetAttribute("myAttribute"));
        Console.WriteLine("GetAttr: " + textInput.GetAttribute("selectionDirection"));


        textInput.SendKeys("Test Message");
        Console.WriteLine("GetText: " + textInput.Text);
        Console.WriteLine("GetAttr: " + textInput.GetAttribute("value"));


        var link = drv.FindElement(By.Id("link"));
        var select = new SelectElement(drv.FindElement(By.Name("currency_code")));

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

    [Test]
    public void TransformationTest()
    {
        drv.Navigate().GoToUrl("http://css3.bradshawenterprises.com/transforms/");
        var element = drv.FindElement(By.Id("rotate"));

        Console.WriteLine("Size before: " + element.Size);
        Console.WriteLine("Position  before: " + element.Location);

        Console.WriteLine("Size after: " + element.Size);
        Console.WriteLine("Position after: " + element.Location);

        // Size before: {Width=92, Height=202}
        // Position  before: {X=525,Y=655}
        // Size after: {Width=92, Height=202}
        // Position after: {X=444,Y=658}

        // Size before: {Width=208, Height=208}
        // Position  before: {X=556,Y=655}
        // Size after: {Width=92, Height=202}
        // Position after: {X=614,Y=658}
    }

    [Test]
    public void BrowserPropertiesTest()
    {
        Console.WriteLine("Title: " + drv.Title);
        Console.WriteLine("Url: " + drv.Url);
        Console.WriteLine("PageHTML: " + drv.PageSource);
    }
}