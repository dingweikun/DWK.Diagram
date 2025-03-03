using System.Threading.Tasks;
using Ursa.Controls;

namespace DWK.Diagram.Views;

public partial class StartupWindow : SplashWindow
{
    public StartupWindow()
    {
        InitializeComponent();
    }

    protected override async Task<Window?> CreateNextWindow()
    {
        return new MainWindow();
    }
}