using System.Drawing;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTraining;

[TestFixture]
public class WindowOperations : BaseTest
{
    [Test]
    public void BrowserWindowTest()
    {
        drv.Navigate().GoToUrl("https://google.com");
        Console.WriteLine("position: " + drv.Manage().Window.Position);
        Console.WriteLine("size:" + drv.Manage().Window.Size);
        drv.Manage().Window.Position = new Point(100, 50);
        drv.Manage().Window.Size = new Size(1000, 1000);
        Console.WriteLine("position2: " + drv.Manage().Window.Position);
        Console.WriteLine("size2:" + drv.Manage().Window.Size);
        Console.WriteLine("Title:" + drv.Title);
        drv.Manage().Window.Minimize();
        drv.Manage().Window.Maximize();
        drv.Manage().Window.FullScreen();
    }

    [Test]
    public void BrowserTabs()
    {
        var handles = drv.WindowHandles;
        var handle = drv.CurrentWindowHandle;
        Console.WriteLine(handles.Count);
        Console.WriteLine(handle);

        drv.SwitchTo().NewWindow(WindowType.Tab);
        drv.Navigate().GoToUrl("https://google.com");
        drv.SwitchTo().NewWindow(WindowType.Window);
        drv.Navigate().GoToUrl("https://selenium.dev");

        Console.WriteLine(drv.WindowHandles.Count);
        Console.WriteLine(drv.CurrentWindowHandle);
        Console.WriteLine(drv.CurrentWindowHandle);
        drv.SwitchTo().Window(handle);
        drv.Close();
    }

    [Test]
    public void AlertsTest()
    {
        drv.Navigate().GoToUrl(DemoHtmlUrl());
        drv.FindElement(By.Id("alert")).Click();

        var alert = drv.SwitchTo().Alert();
        
        alert.SendKeys("Message");
        alert.Dismiss();

        drv.FindElement(By.Id("text")).SendKeys("Test");
    }


    [Test]
    public void IFramesTest()
    {
        drv.Navigate().GoToUrl("http://jsbin.com/bicukojabe/edit?html,output");

        drv.SwitchTo().Frame(wait.Until(ExpectedConditions.ElementExists(By.CssSelector("iframe.stretch"))));
        drv.SwitchTo().Frame(wait.Until(ExpectedConditions.ElementExists(By.Name("JS Bin Output "))));

        drv.FindElement(By.Id("test")).SendKeys("My Input Text");

        drv.SwitchTo().DefaultContent();

        drv.FindElement(By.CssSelector("center a")).Click();
    }

    [Test]
    public void UploadTest()
    {
        drv.Navigate().GoToUrl($"{Environment.GetEnvironmentVariable("ENT_QA_BASE_URL")}/CorpNet/Login.aspx");

        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("username")))
            .SendKeys(Environment.GetEnvironmentVariable("ENT_QA_USER"));

        drv.FindElement(By.Id("password")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_PASS"));
        drv.FindElement(By.Name("_companyText")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_COMPANY"));
        drv.FindElement(By.CssSelector("input.btn.login-submit-button")).Click();

        wait.Until(
            ExpectedConditions.ElementIsVisible(By.CssSelector("div.menu-secondary ul li.menu-user a.menu-drop")));
        
        drv.Navigate().GoToUrl($"{Environment.GetEnvironmentVariable("ENT_QA_BASE_URL")}/corpnet/employee/myprofile.aspx");
        
        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".page-actions-wrapper span.arrow"))).Click();
        drv.FindElement(By.CssSelector("li[data-action=MainFpoQvArea_changeImage] a")).Click();
        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".modal .modal-footer .id-btn-save")));

        
        //Upload file
        var path = Path.Combine(Environment.CurrentDirectory, @"Resources", "avatar.png");
        drv.FindElement(By.CssSelector("input[type=file]")).SendKeys(path);

        
        drv.FindElement(By.CssSelector(".modal .modal-footer .id-btn-save")).Click();
        
    }

}