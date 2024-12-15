namespace DWK.Diagram.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        DataContext = ViewModelLocator.GetViewModel(this);
    }
}