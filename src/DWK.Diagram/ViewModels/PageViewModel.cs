using DWK.Diagram.Services;

namespace DWK.Diagram.ViewModels;

public class PageViewModel : ViewModelBase
{
    public IDiagramDocService DocService { get; }


    public PageViewModel(IDiagramDocService docService)
    {
        DocService = docService ?? throw new ArgumentNullException(nameof(docService));
    }
}