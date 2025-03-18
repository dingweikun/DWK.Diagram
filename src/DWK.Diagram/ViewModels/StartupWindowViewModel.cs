using Irihi.Avalonia.Shared.Contracts;

namespace DWK.Diagram.ViewModels;

public partial class StartupWindowViewModel : ViewModelBase, IDialogContext
{
    #region interface IDialogContext impl

    public void Close()
    {
        RequestClose?.Invoke(this, false);
    }

    public event EventHandler<object?>? RequestClose;

    #endregion
}