using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace DWK.Diagram.Views;

public partial class PageView : ViewBase
{
    public PageView()
    {
        InitializeComponent();
    }
    
    private void CloseTabButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.TemplatedParent is TabItem tabItem)
        {
            var tabControl = tabItem.Parent as TabControl;
            if (tabControl != null)
            {
                tabControl.Items.Remove(tabItem);
            }
        }
    }

}