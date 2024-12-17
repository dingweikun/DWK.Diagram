namespace DWK.Diagram.Views;

public abstract class ViewBase : Avalonia.Controls.UserControl
{
    protected ViewBase()
    {
        DataContext = ViewModels.ViewModelLocator.GetViewModel(this);
    }
}