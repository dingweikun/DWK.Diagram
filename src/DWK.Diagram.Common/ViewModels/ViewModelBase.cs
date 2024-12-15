using CommunityToolkit.Mvvm.ComponentModel;

namespace DWK.Diagram.ViewModels;

public abstract class ViewModelBase : ObservableRecipient
{
    public Guid VmId { get; } = Guid.NewGuid();

    protected ViewModelBase()
    {
        IsActive = true;
    }
}