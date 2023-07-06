using SeleniumTraining.LayeredStructure.BusinessLogic;

namespace SeleniumTraining.LayeredStructure;

[TestFixture]
public class SearchAndPickUpNewWoTest
{
    private CorrigoEnterpriseApplication app;
    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        app = new CorrigoEnterpriseApplication();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        app.CloseApp();
    }

    [Test]
    public void SearchAndPickupWo()
    {
        const string pickUpComment = "Autotest";

        app.LoginWithDefaultUser();
        app.OpenWoList();
        app.CleanUpWoListFilters();
        app.SetNewFiltersToStatusAndAssignee();
        app.SetStatusFilterToNew();
        app.SetAssigneeFilterToUser();
        app.ApplyFilters();

        var woNumber = app.OpenFirstWoFromTheList();

        app.PickUpWo(pickUpComment);

        Assert.Multiple(() =>
        {
            Assert.That(app.GetActivityLogActionTitle(), Is.EqualTo("Picked Up"));
            Assert.That(app.GetActivityLogComment(), Is.EqualTo(pickUpComment));
        });

        app.CloseWoWindow();

        Assert.That(app.GetWoStatusFromWoList(woNumber), Is.EqualTo("Open"));
    }
}