using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DWK.Diagram.Views;

public partial class ExplorerPanelView : UserControl
{
    public ExplorerPanelView()
    {
        InitializeComponent();
        DataContext = ViewModelLocator.GetViewModel(this);
    }
}