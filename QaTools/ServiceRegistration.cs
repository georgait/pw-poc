using QaTools.Options;

namespace QaTools;

internal static class ServiceRegistration
{
    public static async Task RegisterPlaywright(this IObjectContainer container,
        Action<ContextOptions>? options = null)
    {
        var ctxOptions = new ContextOptions();
        container.RegisterInstanceAs(ctxOptions);
        options?.Invoke(ctxOptions);

        var playwright = await Playwright.CreateAsync();

        var browser = await playwright.Chromium.LaunchAsync(new()
        {
            Headless = ctxOptions.Headless
        });

        IBrowserContext context;
        if (!string.IsNullOrEmpty(ctxOptions.VideosDir))
        {
            context = await browser.NewContextAsync(new()
            {
                RecordVideoDir = ctxOptions.VideosDir
            });
        }
        else
        {
            context = await browser.NewContextAsync();
        }

        if (ctxOptions.TraceViewEnabled)
        {
            // Start tracing before creating / navigating a page.
            await context.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            }); 
        }

        var page = await context.NewPageAsync();

        container.RegisterInstanceAs(playwright);
        container.RegisterInstanceAs(browser);
        container.RegisterInstanceAs(page);
        container.RegisterInstanceAs(context);  
    }

    public static void RegisterConfiguration(this IObjectContainer container)
    {
        var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
                .AddEnvironmentVariables()
                .Build();

        container.RegisterInstanceAs<IConfiguration>(config);
    }
}
