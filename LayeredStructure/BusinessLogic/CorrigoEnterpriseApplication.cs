using OpenQA.Selenium.Chrome;
using SeleniumTraining.LayeredStructure.PageObjects;

namespace SeleniumTraining.LayeredStructure.BusinessLogic;

public class CorrigoEnterpriseApplication
{
    private readonly ApplicationContext _context;
    private readonly FiltersPage _filtersPage;

    private readonly LoginPage _loginPage;
    private readonly WoListPage _woListPage;
    private readonly WoPage _woPage;

    public CorrigoEnterpriseApplication()
    {
        var driver = new ChromeDriver();

        _context = new ApplicationContext
        {
            Driver = driver,
            BaseUrl = Environment.GetEnvironmentVariable("ENT_QA_BASE_URL")!,
            Username = Environment.GetEnvironmentVariable("ENT_QA_USER")!,
            Password = Environment.GetEnvironmentVariable("ENT_QA_PASS")!,
            Company = Environment.GetEnvironmentVariable("ENT_QA_COMPANY")!
        };

        _loginPage = new LoginPage(_context);
        _filtersPage = new FiltersPage(_context);
        _woListPage = new WoListPage(_context);
        _woPage = new WoPage(_context);
    }


    public CorrigoEnterpriseApplication LoginWithDefaultUser()
    {
        _loginPage.LoginWithDefaultUser();
        return this;
    }


    public void CloseApp()
    {
        _context.Driver.Quit();
    }


    public CorrigoEnterpriseApplication OpenWoList()
    {
        _woListPage.Open();
        return this;
    }

    public CorrigoEnterpriseApplication CleanUpWoListFilters()
    {
        _woListPage.CleanUpWoListFilters();
        return this;
    }

    public CorrigoEnterpriseApplication SetNewFiltersToStatusAndAssignee()
    {
        _filtersPage.Open();
        _filtersPage.SetNewFiltersToStatusAndAssignee();
        _filtersPage.Close();
        return this;
    }

    public CorrigoEnterpriseApplication SetStatusFilterToNew()
    {
        _woListPage.SetStatusFilterToNew();
        return this;
    }

    public CorrigoEnterpriseApplication SetAssigneeFilterToUser()
    {
        _woListPage.SetAssigneeFilterToUser();
        return this;
    }

    public CorrigoEnterpriseApplication ApplyFilters()
    {
        _woListPage.ApplyFilters();
        return this;
    }

    public string OpenFirstWoFromTheList()
    {
        return _woListPage.OpenFirstWoFromTheList();
    }

    public CorrigoEnterpriseApplication PickUpWo(string pickUpComment)
    {
        _woPage.PickUpWo(pickUpComment);
        return this;
    }

    public string GetActivityLogActionTitle()
    {
        return _woPage.GetActivityLogActionTitle();
    }

    public string GetActivityLogComment()
    {
        return _woPage.GetActivityLogComment();
    }

    public CorrigoEnterpriseApplication CloseWoWindow()
    {
        _woPage.Close();
        return this;
    }

    public string GetWoStatusFromWoList(string woNumber)
    {
        return _woListPage.GetWoStatusFromWoList(woNumber);
    }
}