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
        _context = new ApplicationContext();

        var options = new ChromeOptions();
        options.AddArguments("start-maximized");
        var driver = new ChromeDriver(options);


        _context.driver = driver;
        _context.baseUrl = Environment.GetEnvironmentVariable("ENT_QA_BASE_URL");
        _context.username = Environment.GetEnvironmentVariable("ENT_QA_USER");
        _context.password = Environment.GetEnvironmentVariable("ENT_QA_PASS");
        _context.company = Environment.GetEnvironmentVariable("ENT_QA_COMPANY");

        _loginPage = new LoginPage(_context);
        _filtersPage = new FiltersPage(_context);
        _woListPage = new WoListPage(_context);
        _woPage = new WoPage(_context);
    }


    public void LoginWithDefaultUser()
    {
        _loginPage.LoginWithDefaultUser();
    }


    public void CloseApp()
    {
        _context.driver.Quit();
    }


    public void OpenWoList()
    {
        _woListPage.Open();
    }

    public void CleanUpWoListFilters()
    {
        _woListPage.CleanUpWoListFilters();
    }

    public void SetNewFiltersToStatusAndAssignee()
    {
        _filtersPage.Open();
        _filtersPage.SetNewFiltersToStatusAndAssignee();
        _filtersPage.Close();
    }

    public void SetStatusFilterToNew()
    {
        _woListPage.SetStatusFilterToNew();
    }

    public void SetAssigneeFilterToUser()
    {
        _woListPage.SetAssigneeFilterToUser();
    }

    public void ApplyFilters()
    {
        _woListPage.ApplyFilters();
    }

    public string OpenFirstWoFromTheList()
    {
        return _woListPage.OpenFirstWoFromTheList();
    }

    public void PickUpWo(string pickUpComment)
    {
        _woPage.PickUpWo(pickUpComment);
    }

    public string GetActivityLogActionTitle()
    {
        return _woPage.GetActivityLogActionTitle();
    }

    public string GetActivityLogComment()
    {
        return _woPage.GetActivityLogComment();
    }

    public void CloseWoWindow()
    {
        _woPage.Close();
    }

    public string GetWoStatusFromWoList(string woNumber)
    {
        return _woListPage.GetWoStatusFromWoList(woNumber);
    }
}