using Avalonia.Controls;

namespace DWK.Diagram.Views;

public partial class ShellWindow : Window
{
    protected const double SidePanelDefaultWidth = 280;
    private double _sidePanelSavedWidth = SidePanelDefaultWidth;

    public double SidePanelSavedWidth
    {
        get => _sidePanelSavedWidth;
        set => _sidePanelSavedWidth = value > SidePanelDefaultWidth ? value : SidePanelDefaultWidth;
    }

    public ShellWindow()
    {
        InitializeComponent();
    }

    private void OnSidePanelSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not ListBox listbox) return;

        ColumnDefinition sidePanelColumn = PartGrid.ColumnDefinitions[1];

        if (e.RemovedItems.Count > 0)
            SidePanelSavedWidth = sidePanelColumn.Width.Value;

        if (listbox.SelectedIndex >= 0)
        {
            sidePanelColumn.Width = new GridLength(SidePanelSavedWidth, GridUnitType.Pixel);
            PartSplitter.IsVisible = true;
        }
        else
        {
            sidePanelColumn.Width = new GridLength(0, GridUnitType.Pixel);
            PartSplitter.IsVisible = false;
        }
    }
}