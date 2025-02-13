using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using DWK.Diagram.Models;

namespace DWK.Diagram.Views;

public partial class LeftPanelExplorerView : ViewBase
{
    public event EventHandler<DiagramPage> PageItemDoubleTapped;

    public LeftPanelExplorerView()
    {
        InitializeComponent();
    }

    private void HidePanelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.IsVisible = false;
    }

    [Obsolete("模拟打开工程")]
    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var service = (DataContext as LeftPanelExplorerViewModel).DocService;
        if (service is null) return;

        service.OpenProject("../../../模拟路径");
    }

    private void Button_TreeViewItemsExpand(object? sender, RoutedEventArgs e)
    {
        (PART_TreeViewRootItem as TreeViewItem).IsExpanded = true;
    }

    private void Button_TreeViewItemsCollapse(object? sender, RoutedEventArgs e)
    {
        (PART_TreeViewRootItem as TreeViewItem).IsExpanded = false;
    }

    private void TreeView_DoubleTapped(object? sender, TappedEventArgs e)
    {
        var item = (e.Source as Control)?.FindAncestorOfType<TreeViewItem>();
        if (item is { DataContext: DiagramPage page })
            PageItemDoubleTapped?.Invoke(this, page);
    }
}