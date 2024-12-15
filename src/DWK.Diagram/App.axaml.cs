using System.Threading;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DWK.Diagram;

public partial class App : Application, IServiceProviderApp
{
    private readonly IHost _host;

    public IServiceProvider Services => _host.Services;

    public App()
    {
        _host = CreateHost();

        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            // TODO: catch unhandled exception   
            var file = "UnhandledException_" + DateTime.Now.ToString("yyyy_MM_dd HH_mm_ss");
            var content = e.ExceptionObject.ToString();
            System.IO.File.WriteAllText(file, content);
        };
    }

    private static IHost CreateHost()
    {
        // create host builder
        var builder = Host.CreateEmptyApplicationBuilder(null);

        // set host configuration source
        builder.Configuration.Sources.Clear();
        builder.Configuration
            .AddJsonFile("appsettings.json", true, false);

        // set logging
        builder.Logging
            .AddConfiguration(builder.Configuration.GetSection("Logging"))
            .AddSimpleConsole();

        // dependency injection 
        ConfigureServices(builder.Services);

        return builder.Build();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ExplorerPanelViewModel>();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // In design mode (uses the IDE previewer process), ApplicationLifetime is null
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();

            // create main window
            desktop.MainWindow = new MainWindow();

            // start host
            var hostCts = new CancellationTokenSource();
            _host.RunAsync(hostCts.Token);

            // stop host when window shutdown
            desktop.ShutdownRequested += (_, _) => hostCts.Cancel();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}