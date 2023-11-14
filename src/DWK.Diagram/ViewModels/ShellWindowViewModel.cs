using Avalonia;
using Avalonia.Styling;
using DWK.Diagram.Models;
using DWK.Diagram.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace DWK.Diagram.ViewModels;

internal class ShellWindowViewModel : BindableBase, INavigationAware
{
    private readonly IEventAggregator _aggregator;
    private readonly IRegionManager _regionManager;

    public ShellWindowViewModel(IRegionManager regionManager, IEventAggregator aggregator)
    {
        _regionManager = regionManager;
        _regionManager.RegisterViewWithRegion(RegionNames.DiagramRegion, nameof(DiagramView));
        //_regionManager.RequestNavigate(RegionNames.SidePanelRegion, nameof(PageListPanel));

        _aggregator = aggregator;
        _aggregator.GetEvent<ShiftSidePanelEvent>().Subscribe(panel =>
        {
            _regionManager.RequestNavigate(RegionNames.SidePanelRegion, panel);
        });
    }


    public string ShellTitle => "DWK Diagram App 2023";

    public SidePanelItem[] SidePanelItems { get; } = new[]
    {
        //new SidePanelItem(SidePanelTitles.PageLsit, "Document"),
        //new SidePanelItem(SidePanelTitles.ModuleLsit, "List"),
        //new SidePanelItem(SidePanelTitles.ModuleLibrary, "ViewAll"),
        new SidePanelItem(nameof(PageListPanel), "Document"),
        new SidePanelItem(nameof(ModuleListPanel), "List"),
        new SidePanelItem(nameof(ModuleLibraryPanel), "ViewAll"),

    };

    public DelegateCommand ShiftThemeCommand { get; } = new DelegateCommand(() =>
    {
        var app = Application.Current!;
        var isDark = app.ActualThemeVariant.Key == ThemeVariant.Dark.Key;
        app.RequestedThemeVariant = isDark ? ThemeVariant.Light : ThemeVariant.Dark;
    });




    public bool IsNavigationTarget(NavigationContext navigationContext) => true;

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
        //throw new System.NotImplementedException();
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        //throw new System.NotImplementedException();
    }
}

public record SidePanelItem(string PanelTitle, string IconSymbol);