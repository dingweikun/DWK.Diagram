using DWK.Diagram.Adornment;
using Northwoods.Go.Models;

namespace DWK.Diagram.Node;

internal class SwitchNodeTemplate : IElecNodeTemplateMake<SwitchNodeData>
{
    public static Northwoods.Go.Node Instance { get; } = new SwitchNodeTemplate().Make();

    public SwitchNodeData NodeDataSample { get; } = new();

    public Northwoods.Go.Node Make()
    {
        return new Northwoods.Go.Node(PanelLayoutSpot.Instance)
            {
                SelectionElementName = "SHAPE",
                LocationElementName = "SHAPE", LocationSpot = Spot.BottomLeft,
                ContextMenu = ElecNodeContextToolbar.Make(NodeDataSample.Category)
            }
            .Bind(new Binding("Angle", nameof(NodeDataSample.IsVertical), val => val is true ? 90 : 0))
            .Add(
                new Shape("Rectangle")
                    { Name = "SHAPE", Fill = "transparent", Width = 80, Height = 30, StrokeWidth = 0 },
                new TextBlock
                    {
                        TextAlign = TextAlign.Center, Alignment = new Spot(0.5, 1, 0, 10), AlignmentFocus = Spot.Top,
                    }
                    .Bind(new Binding("Angle", nameof(NodeDataSample.IsVertical), val => val is true ? -90 : 0))
                    .Bind(new Binding("Text", nameof(NodeDataSample.Tag)).MakeTwoWay())
                    .Bind(new Binding("Font", nameof(ElecDiagramTheme.TagFont)).OfModel())
                    .Bind(new Binding("Stroke", nameof(ElecDiagramTheme.DefaultTextBrush)).OfModel()),
                new Shape
                    {
                        Stroke = "green", StrokeWidth = 6, StrokeJoin = LineJoin.Bevel,
                        GeometryString = "M0,0 L20,0 L20,-30 M80,0 L70,0 L10,-20"
                    }
                    .Bind(new Binding("Visible", nameof(SwitchNodeData.Opened), o => o is not true, v => v is true).MakeTwoWay()),
                new Shape
                    {
                        Stroke = "red", StrokeWidth = 6, StrokeJoin = LineJoin.Bevel,
                        GeometryString = "M0,0 L20,0 M80,0 L70,0 L20,-30"
                    }
                    .Bind(new Binding("Visible", nameof(SwitchNodeData.Opened)).MakeTwoWay()),
                new Shape("Rectangle")
                    {
                        Height = 8, Width = 8, StrokeWidth = 0, Alignment = new Spot(0, 1), AlignmentFocus = Spot.Center
                    }
                    .Bind(new Binding("Fill", nameof(ElecDiagramTheme.PortBrush)).OfModel()),
                new Shape("Rectangle")
                    {
                        Height = 8, Width = 8, StrokeWidth = 0, Alignment = new Spot(1, 1), AlignmentFocus = Spot.Center
                    }
                    .Bind(new Binding("Fill", nameof(ElecDiagramTheme.PortBrush)).OfModel())
            );
    }
}