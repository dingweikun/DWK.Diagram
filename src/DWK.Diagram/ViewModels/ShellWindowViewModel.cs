using Avalonia;
using Avalonia.Styling;
using DWK.Diagram.Models;
using DWK.Diagram.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace DWK.Diagram.ViewModels;

internal class ShellWindowViewModel : BindableBase
{
    private readonly IRegionManager _regionManager;
    private SidePanelItem? _currrntSidePanelItem;

    public ShellWindowViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        _regionManager.RegisterViewWithRegion(RegionNames.DiagramRegion, nameof(DiagramView));
    }

    public string ShellTitle => "DWK Diagram App 2023";

    public SidePanelItem[] SidePanelItems { get; } =
    {
        new SidePanelItem(nameof(PageListPanel), "Document"),
        new SidePanelItem(nameof(ModuleListPanel), "List"),
        new SidePanelItem(nameof(ModuleLibraryPanel), "ViewAll")
    };

    public SidePanelItem? CurrentSidePanelItem
    {
        get => _currrntSidePanelItem;
        set
        {
            if (SetProperty(ref _currrntSidePanelItem, value) && value.HasValue)
                _regionManager.RequestNavigate(RegionNames.SidePanelRegion, value.Value.PanelName);
        }
    }

    public DelegateCommand ShiftThemeCommand { get; } = new(() =>
    {
        var app = Application.Current!;
        var isDark = app.ActualThemeVariant.Key == ThemeVariant.Dark.Key;
        app.RequestedThemeVariant = isDark ? ThemeVariant.Light : ThemeVariant.Dark;
    });
}

public record struct SidePanelItem(string PanelName, string IconSymbol);