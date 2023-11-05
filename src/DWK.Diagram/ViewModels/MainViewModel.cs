using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;

namespace DWK.Diagram.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(ShowPanel))] [NotifyPropertyChangedFor(nameof(PanelTitle))]
    private int _panelIndex;

    public bool ShowPanel => PanelIndex >= 0;

    public string PanelTitle => PanelIndex switch
    {
        0 => "Pages",
        1 => "Components",
        2 => "Modules",
        _ => string.Empty
    };

    public ICommand ToggleThemeCommand { get; } = new RelayCommand(() =>
    {
        var isDark = Application.Current!.ActualThemeVariant.Key == ThemeVariant.Dark.Key;
        Application.Current!.RequestedThemeVariant = isDark ? ThemeVariant.Light : ThemeVariant.Dark;
    });


    public ObservableCollection<string> Documents { get; } = new(new[]
    {
        "Page-1", "Page-2"
    });


}public class DocumentItem
{
    public string Header { get; set; }

    public IconSource IconSource { get; set; }

    public string Content { get; set; }
}