using Ursa.Controls;

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
    private void OpenPage(IDiagramPage diagramPage) => DocService.OpenPage(diagramPage);
}