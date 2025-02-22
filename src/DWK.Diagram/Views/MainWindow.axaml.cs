using Avalonia.Styling;
using Ursa.Controls;
using Ursa.Themes.Semi;

namespace DWK.Diagram.Views;

public partial class MainWindow : UrsaWindow
{
    public MainWindow()
    {
        InitializeComponent();
        InitializeThemeListBox();
    }

    private void InitializeThemeListBox()
    {
        PART_ThemeListBox.ItemsSource = new[]
        {
            ThemeVariant.Light,
            SemiTheme.Desert,
            ThemeVariant.Dark,
            SemiTheme.Dusk,
            SemiTheme.Aquatic,
            SemiTheme.NightSky,
        };
        PART_ThemeListBox.SelectedItem = Application.Current?.ActualThemeVariant;
        PART_ThemeListBox.SelectionChanged += (sender, args) =>
        {
            if (sender is ListBox { SelectedItem: ThemeVariant theme } && Application.Current != null)
            {
                Application.Current.RequestedThemeVariant = theme;
            }
        };
    }
}