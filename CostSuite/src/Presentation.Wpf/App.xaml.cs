using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CostSuite.Presentation.Wpf;

public partial class App : Application
{
    public static ServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        Services = Bootstrapper.ConfigureServices();
        base.OnStartup(e);
    }
}
