using Avalonia.Interactivity;

namespace DWK.Diagram.Views;

public partial class LeftPanelComponentView : ViewBase
{
    public LeftPanelComponentView()
    {
        InitializeComponent();
    }
    
    private void HidePanelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.IsVisible = false;
    }
}