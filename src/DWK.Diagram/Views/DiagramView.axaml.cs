using Avalonia.Controls;
using DWK.Diagram.Models;
using Prism.Events;

namespace DWK.Diagram;

public partial class DiagramView : UserControl
{
    private readonly IEventAggregator _aggregator;

    public DiagramView(IEventAggregator aggregator)
    {
        InitializeComponent();
        _aggregator = aggregator;
        _aggregator.GetEvent<DiagramInitEvent>().Subscribe(agent =>
        {
            agent.Reset(PartDiagramControl.Diagram);
        });
    }
}