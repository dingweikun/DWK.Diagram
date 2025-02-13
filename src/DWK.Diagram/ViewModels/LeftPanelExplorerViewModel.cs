using CommunityToolkit.Mvvm.Input;
using DWK.Diagram.Models;
using DWK.Diagram.Services;

namespace DWK.Diagram.ViewModels;

public partial class LeftPanelExplorerViewModel : ViewModelBase
{
    [ObservableProperty] private string _title = "Project";

    public IDiagramDocService DocService { get; }

    public LeftPanelExplorerViewModel(IDiagramDocService docService)
    {
        DocService = docService ?? throw new ArgumentNullException(nameof(docService));
    }

    [RelayCommand]
    private void OpenPage(DiagramPage page)
    {
        DocService.OpenPage(page.Id);
    }
}