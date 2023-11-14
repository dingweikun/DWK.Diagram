using Avalonia.Controls;
using DWK.Diagram.Models;
using DWK.Diagram.ViewModels;
using Prism.Events;

namespace DWK.Diagram.Views;

public partial class ShellWindow : Window
{
    protected const double SidePanelDefaultWidth = 300;
    private readonly IEventAggregator? _aggregator;
    private double _sidePanelSavedWidth = SidePanelDefaultWidth;

    public double SidePanelSavedWidth
    {
        get => _sidePanelSavedWidth;
        set => _sidePanelSavedWidth = value > 0 ? value : SidePanelDefaultWidth;
    }

    public ShellWindow(IEventAggregator aggregator)
    {
        _aggregator = aggregator;
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

        // 发布事件
        string panelTitle = (listbox.SelectedItem as SidePanelItem)?.PanelTitle ?? string.Empty;
        _aggregator?.GetEvent<ShiftSidePanelEvent>().Publish(panelTitle);
    }
}