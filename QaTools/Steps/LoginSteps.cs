namespace QaTools.Steps;

[Binding]
public class LoginSteps
{
    private readonly IConfiguration _configuration;
    private readonly IPage _page;
    
    private string? _username;
    private string? _pass;
    private string? _url;

    public LoginSteps(IConfiguration configuration, IPage page)
    {
        _configuration = configuration;
        _page = page;
    }


    [Given(@"the user ""([^""]*)"" navigates to ""([^""]*)""")]
    public async Task GivenTheUserNavigatesTo(string testUser, string baseUrl)
    {
        _username = _configuration[$"users:{testUser}:username"]!;
        _pass = _configuration[$"users:{testUser}:password"]!;
        _url = _configuration[baseUrl]!;

        await _page.GotoAsync(_url!);
    }

    [When("he types the username and password")]
    public async Task WhenHeTypesTheUsernameAndPassword()
    {
        await _page.GetByPlaceholder("UserName").FillAsync(_username!);
        await _page.GetByPlaceholder("Password").FillAsync(_pass!);
        await _page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
    }

    [Then(@"he should see (.*) books")]
    public async Task ThenHeShouldSeeBooks(int numOfBooks)
    {
        var locators = _page.GetByRole(AriaRole.Img, new() { Name = "image" });
        await Assertions.Expect(locators).ToHaveCountAsync(numOfBooks);
    }

    [Then("he logs out")]
    public async Task ThenHeLogsOut()
    {
        await _page.GetByRole(AriaRole.Button, new() { Name = "Log out" }).ClickAsync();
    }

    [Then(@"he should see the ""([^""]*)"" title")]
    public async Task ThenHeShouldSeeTheTitle(string login)
    {
        var locator = _page.GetByText("Login").First;
        await Assertions
            .Expect(locator)
            .ToContainTextAsync(login, new LocatorAssertionsToContainTextOptions() { Timeout= 10000 });
    }

}
