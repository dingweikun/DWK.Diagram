using System;
using Avalonia;
using Avalonia.Media;

namespace DWK.Diagram;

internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .With(new FontManagerOptions
            {
                DefaultFamilyName = "avares://DWK.Diagram/Assets/Fonts/SourceHanSansSC-Normal.otf#Source Han Sans SC",
                FontFallbacks = new[]
                {
                    new FontFallback
                    {
                        FontFamily = "avares://DWK.Diagram/Assets/Fonts/SourceHanSansSC-Normal.otf#Source Han Sans SC"
                    }
                }
            });
    }
}