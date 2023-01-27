using QaTools.Options;

[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace QaTools.Hooks;

[Binding]
public class Hooks
{
    [BeforeScenario]
    public async Task BeforeScenario(IObjectContainer container)
    {
        container.RegisterConfiguration();

        var configuration = container.Resolve<IConfiguration>();

        await container.RegisterPlaywright(opt =>
        {
            opt.Headless = bool.Parse(configuration["context:headless"]!);
            opt.VideosDir = configuration["context:recordVideoDir"];
            opt.TraceViewEnabled = bool.Parse(configuration["context:traceViewEnabled"]!);
        });
    }

    [AfterScenario]
    public async Task AfterScenario(IObjectContainer container)
    {
        var context = container.Resolve<IBrowserContext>();

        var options = container.Resolve<ContextOptions>();
        if (options.TraceViewEnabled)
        {
            // Stop tracing and export it into a zip archive.
            await context.Tracing.StopAsync(new()
            {
                Path = "trace.zip"
            });
        }

        await context.CloseAsync();
        await context.DisposeAsync();

        var browser = container.Resolve<IBrowser>();
        await browser.CloseAsync();
        
        var playwright = container.Resolve<IPlaywright>();
        playwright.Dispose();
    }
}
