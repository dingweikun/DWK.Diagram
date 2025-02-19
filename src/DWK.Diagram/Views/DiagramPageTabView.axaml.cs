using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DWK.Diagram.Views;

public partial class DiagramPageTabView : UserControl
{
    public DiagramPageTabView()
    {
        InitializeComponent();

        DataContext = new DiagramPageTabViewModel();
    }
}