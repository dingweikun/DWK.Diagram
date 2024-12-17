using Avalonia.Interactivity;

namespace DWK.Diagram.Views;

public partial class LeftPanelExplorerView : ViewBase
{
    public LeftPanelExplorerView()
    {
        InitializeComponent();
    }

    private void HidePanelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.IsVisible = false;
    }
}