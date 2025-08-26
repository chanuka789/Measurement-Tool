using Microsoft.Extensions.DependencyInjection;

namespace CostSuite.Presentation.Wpf;

public static class Bootstrapper
{
    public static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        // TODO: register services
        return services.BuildServiceProvider();
    }
}
