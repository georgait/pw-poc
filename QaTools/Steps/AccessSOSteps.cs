namespace QaTools.Steps
{
    [Binding]
    public class AccessSOSteps
    {
        private readonly IPage _page;
        private readonly IConfiguration _configuration;

        public AccessSOSteps(IPage page, IConfiguration configuration)
        {
            _page = page;
            _configuration = configuration;
        }

        [Given(@"navigate to target url")]
        public async Task GivenNavigateToTargetUrl()
        {
            var url = _configuration["so"];
            await _page.GotoAsync(url!);
        }

        [When(@"accept all cookies")]
        public async Task WhenAcceptAllCookies()
        {
            await Task.Delay(1000);
            await _page.GetByRole(AriaRole.Button, new() { Name = "Accept all cookies" }).ClickAsync();
            await Task.Delay(3000);
        }

        [Then(@"go to questions")]
        public async Task ThenGoToQuestions()
        {
            await _page.GetByRole(AriaRole.Menuitem).First.ClickAsync();

            await _page.Locator("#nav-questions").ClickAsync();
        }

        [Then("select bounty tab")]
        public async Task ThenSelectBountyTab()
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = "Bountied 270" }).ClickAsync();
        }

        [Then("select the first post")]
        public async Task ThenSelectTheFirstPost()
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = "How to block implicitly casting from `any` to a stronger type" }).ClickAsync();
        }

    }
}
