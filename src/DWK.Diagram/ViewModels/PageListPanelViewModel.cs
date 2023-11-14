using DWK.Diagram.Base;
using DWK.Diagram.Logic;
using DWK.Diagram.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace DWK.Diagram.ViewModels;

public class PageListPanelViewModel : BindableBase
{
    private readonly IEventAggregator _aggregator;

    private string _openedPage = string.Empty;

    public PageListPanelViewModel(IEventAggregator aggregator)
    {
        _aggregator = aggregator;

        OpenPageCommand = new DelegateCommand(OpenPage);
    }

    public string PanelTitle => "Page List".ToUpper();

    public string OpenedPage
    {
        get => _openedPage;
        set
        {
            if (SetProperty(ref _openedPage, value))
                OpenPage();
        }
    }

    public string[] Pages { get; } = { "Page-1", "Page-2", "Page-3", "Page-4", "Page-5" };

    public DelegateCommand OpenPageCommand { get; }

    private void OpenPage()
    {
        var agent = new LogicDiagramAgent();
        _aggregator.GetEvent<DiagramInitEvent>().Publish(agent);
    }
}

public class ModuleListPanelViewModel : BindableBase
{
    public string PanelTitle => "Module List".ToUpper();
}

public class ModuleLibraryPanelViewModel : BindableBase
{
    public string PanelTitle => "Module Library".ToUpper();
}