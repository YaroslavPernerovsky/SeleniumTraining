using OpenQA.Selenium;

namespace SeleniumTraining;

[TestFixture]
public class EnterpriseLoginTest : BaseTest
{
    [Test]
    public void LoginTest()
    {
        drv.Navigate().GoToUrl(Environment.GetEnvironmentVariable("ENT_QA_URL"));
        drv.FindElement(By.Name("username")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_USER"));
        drv.FindElement(By.Name("password")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_PASS"));
        // drv.FindElement(By.Name("password")).Clear();
        // drv.FindElement(By.Id("RememberMe")).Clear();
        // drv.FindElement(By.Name("password")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_PASS"));
        drv.FindElement(By.Name("_companyText")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_COMPANY"));
        //drv.FindElement(By.TagName("form")).Submit();

        drv.FindElement(By.CssSelector("input[type=submit]")).Click(); //SendKeys(Keys.Enter);

        Thread.Sleep(4000);

        var version = drv.FindElement(By.CssSelector(".menu-logo img")).GetAttribute("title");

        Assert.That(version, Is.EqualTo("Corrigo Enterprise 9.99.0.0"));
    }
}