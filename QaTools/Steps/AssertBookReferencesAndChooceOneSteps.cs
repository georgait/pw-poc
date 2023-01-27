namespace QaTools;

[Binding]
public class AssertBookReferencesAndChooceOneSteps
{
    private readonly IPage _page;
    private IPage? _page2;

    public AssertBookReferencesAndChooceOneSteps(IPage page)
    {
        _page = page;
    }

    [Then(@"he chooses the ""([^""]*)"" book")]
    public async Task ThenHeChoosesTheBook(string title)
    {
        await Task.Delay(3000);
        await _page.GetByRole(AriaRole.Link, new() { Name = title }).ClickAsync();
    }

    [Then(@"he navigates to website of the book")]
    public async Task ThenHeNavigatesToWebsiteOfTheBook()
    {
        _page2 = await _page.RunAndWaitForPopupAsync(async () =>
        {
            await _page.GetByText("http://www.addyosmani.com/resources/essentialjsdesignpatterns/book/").ClickAsync();
        });
    }

    [Then(@"he selects the last reference")]
    public async Task ThenHeSelectsTheThReference()
    {
        await _page2!
            .GetByRole(AriaRole.Link, new() { Name = "http://www.javaworld.com/javaworld/javaqa/2001-05/04-qa-0525-observer.html" })
            .ClickAsync();
    }

    [Then(@"he confirms that the page not found")]
    public async Task ThenHeConfirmsThatThePageNotFound()
    {
        await _page2!.GetByRole(AriaRole.Heading, new() { Name = "Page not found" }).ClickAsync();
    }

    [Then(@"he closes the window")]
    public async Task ThenHeClosesTheWindow()
    {
        await _page2!.CloseAsync();
    }

    [Then(@"he clicks on ""([^""]*)""")]
    public async Task ThenHeClicksOn(string bookTitle)
    {
        await _page.GetByRole(AriaRole.Button, new() { Name = bookTitle }).ClickAsync();
    }
}
