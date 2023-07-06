using SeleniumTraining.LayeredStructure.BusinessLogic;

namespace SeleniumTraining.LayeredStructure;

[TestFixture]
public class SearchAndPickUpNewWoTest
{
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

    private CorrigoEnterpriseApplication app;

    [Test]
    public void SearchAndPickupWo()
    {
        const string pickUpComment = "Autotest";

        app.LoginWithDefaultUser()
            .OpenWoList()
            .CleanUpWoListFilters()
            .SetNewFiltersToStatusAndAssignee()
            .SetStatusFilterToNew()
            .SetAssigneeFilterToUser()
            .ApplyFilters();

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