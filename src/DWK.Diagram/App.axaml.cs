using Avalonia;
using Avalonia.Markup.Xaml;
using DWK.Diagram.Views;
using Prism.DryIoc;
using Prism.Ioc;

namespace DWK.Diagram;

public class App : PrismApplication
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        base.Initialize();
    }

    protected override AvaloniaObject CreateShell()
    {
        return Container.Resolve<ShellWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<DiagramView>();

        containerRegistry.RegisterForNavigation<PageListPanel>();
        containerRegistry.RegisterForNavigation<ModuleListPanel>();
        containerRegistry.RegisterForNavigation<ModuleLibraryPanel>();
    }
}