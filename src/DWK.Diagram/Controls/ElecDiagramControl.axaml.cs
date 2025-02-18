using Avalonia.Interactivity;
using DWK.Diagram.Node;

namespace DWK.Diagram.Controls;

public partial class ElecDiagramControl : UserControl, IDiagramControl
{
    private readonly ElecDiagramPage _diagramPage;

    public IDiagramPage DiagramPage => _diagramPage;

    public ElecDiagramControl(ElecDiagramPage diagramPage)
    {
        _diagramPage = diagramPage;

        InitializeComponent();
        //InitDiagramControl();
    }

    private void InitDiagramControl()
    {
        var builder = new LogicDiagramBuilder()
            .WithSettings(settings =>
            {
                // settings.LinkColor = "rgba(255,255,255)";
                // settings.ModuleBackColor = "rgba(0,97,204)";
                // settings.ModuleTextColor = "rgba(255,255,255)";
                // settings.NodeTipBackColor = "rgba(255,0,255)";
                // settings.NodeTipForeColor = "rgba(255,255,255)";
                // settings.LinkTipBackColor = "rgba(255,255,0)";
                // settings.LinkTipForeColor = "rgba(255,255,255)";
            });

        //builder.BuildDiagram(PART_GoDiagramControl.Diagram);

        var esbuilder = new ElecDiagramBuilder();
        esbuilder.BuildDiagram(PART_GoDiagramControl.Diagram);

        PART_GoDiagramControl.Diagram.ToolManager.DraggingTool.IsGridSnapEnabled = true;
        PART_GoDiagramControl.Diagram.ToolManager.ResizingTool.IsGridSnapEnabled = true;
        // Testing.Setup3(PART_GoDiagramControl.Diagram);
        // Testing.Setup2(PART_GoDiagramControl.Diagram);
        //Testing.Setup0(PART_GoDiagramControl.Diagram);
        // Testing.Setup(PART_GoDiagramControl.Diagram);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var b = PART_GoDiagramControl.Diagram.Grid.Background.ToString();

        var state = !PART_GoDiagramControl.Diagram.Grid.Visible;

        PART_GoDiagramControl.Diagram.Grid.Visible = state;

        // PART_GoDiagramControl.Background = 
    }

    private void SpyButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (PART_GoDiagramControl.Diagram.Model is not ElecModel m) return;
        if (m.LinkDataSource.FirstOrDefault() is not { } l) return;

        ElecNodeData fromData = m.NodeDataSource.Single(n => n.Key == l.From)!;
        ElecNodeData ToData = m.NodeDataSource.Single(n => n.Key == l.To)!;

        Spy(fromData, l);
        Spy(ToData, l);
    }

    private void Spy(ElecNodeData nodeData, ElecLinkData linkData)
    {
        var link = PART_GoDiagramControl.Diagram.FindLinkForData(linkData);
        if (link == null) return;

        var node = PART_GoDiagramControl.Diagram.FindNodeForData(nodeData);
        if (node == null) return;

        var elinks = node.FindLinksConnected("e");
        var slinks = node.FindLinksConnected("s");

        if (Enumerable.SingleOrDefault<Go.Link>(elinks) is { } el)
        {
            bool ok = el == link;
            if (ok) Console.WriteLine("el ok");
        }

        if (Enumerable.SingleOrDefault<Go.Link>(slinks) is { } sl)
        {
            bool ok = sl == link;
            if (ok) Console.WriteLine("sl ok");
        }
    }

    private void TagButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (PART_GoDiagramControl.Diagram.Model is not { SharedData: ElecDiagramTheme sharedData } model) return;

        model.Commit(m => m.Set(m.SharedData, nameof(ElecDiagramTheme.TagVisible), !sharedData.TagVisible), "Toggle Tag Visible");
    }
}