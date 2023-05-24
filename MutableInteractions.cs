using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTraining;

[TestFixture]
public class MutableInteractions : BaseTest
{
    [Test]
    public void SelectTest()
    {
        drv.Navigate().GoToUrl(DemoHtmlUrl());

        var select = By.Name("currency_code");

        var curr = new SelectElement(drv.FindElement(select));

        curr.SelectByIndex(2);
        curr.SelectByValue("UAH");
    }

    [Test]
    public void ActionsTest()
    {
        drv.Navigate().GoToUrl("http://jqueryui.com/resources/demos/sortable/connect-lists.html");

        var firstList = drv.FindElements(By.CssSelector("#sortable1 li"));
        var secondList = drv.FindElements(By.CssSelector("#sortable2 li"));

        new Actions(drv)
            .DragAndDrop(firstList[0], firstList[3]);

        new Actions(drv)
            .DragAndDrop(firstList[0], secondList[2])
            .Perform();

        var actionBuilder = new ActionBuilder();
        var mouse = new PointerInputDevice(PointerKind.Mouse, "default mouse");

        actionBuilder.AddAction(mouse.CreatePointerMove(firstList[1], 1, 1, TimeSpan.Zero))
            .AddAction(mouse.CreatePointerDown(MouseButton.Left))
            .AddAction(mouse.CreatePointerMove(CoordinateOrigin.Pointer, 0, 50, TimeSpan.FromMilliseconds(100)))
            .AddAction(mouse.CreatePointerUp(MouseButton.Left));

        ((IActionExecutor)drv).PerformActions(actionBuilder.ToActionSequenceList());
        ((IActionExecutor)drv).PerformActions(actionBuilder.ToActionSequenceList());
        ((IActionExecutor)drv).PerformActions(actionBuilder.ToActionSequenceList());
    }

    [Test]
    public void InvisibleTest()
    {
        drv.Navigate().GoToUrl("http://cssglobe.com/lab/style_select/01.html");
        var selectEl = drv.FindElement(By.CssSelector("select"));

        ((IJavaScriptExecutor)drv).ExecuteScript("arguments[0].selectedIndex = 3;", selectEl);
        ((IJavaScriptExecutor)drv).ExecuteScript(
            "arguments[0].selectedIndex = 3; arguments[0].dispatchEvent(new Event('change'))", selectEl);
        ((IJavaScriptExecutor)drv).ExecuteScript("arguments[0].style.opacity=1", selectEl);

        var select = new SelectElement(selectEl);
        select.SelectByIndex(2);
        ((IJavaScriptExecutor)drv).ExecuteScript("arguments[0].style.opacity=0", selectEl);
    }
}