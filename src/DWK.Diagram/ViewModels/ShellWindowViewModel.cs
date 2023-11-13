using Avalonia;
using Avalonia.Styling;
using DWK.Diagram.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace DWK.Diagram.ViewModels;

internal class ShellWindowViewModel : BindableBase
{
    public string ShellTitle => "DWK Diagram App 2023";

    public SidePanelItem[] SidePanelItems { get; } = new[]
    {
        new SidePanelItem(SidePanelTitles.PageLsit, "Document"),
        new SidePanelItem(SidePanelTitles.ModuleLsit, "List"),
        new SidePanelItem(SidePanelTitles.ModuleLibrary, "ViewAll"),
    };

    public DelegateCommand ShiftThemeCommand { get; } = new DelegateCommand(() =>
    {
        var app = Application.Current!;
        var isDark = app.ActualThemeVariant.Key == ThemeVariant.Dark.Key;
        app.RequestedThemeVariant = isDark ? ThemeVariant.Light : ThemeVariant.Dark;
    });
}

public record SidePanelItem(string PanelTitle, string IconSymbol);