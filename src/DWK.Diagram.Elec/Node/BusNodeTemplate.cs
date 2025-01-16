using Northwoods.Go.Models;

namespace DWK.Diagram.Node;

public static class BusNodeTemplate
{
    public static Northwoods.Go.Node Make()
    {
        return new Northwoods.Go.Node(PanelLayoutSpot.Instance)
            {
                SelectionElementName = "SHAPE",
                LocationElementName = "SHAPE", LocationSpot = Spot.TopLeft,
                Resizable = true, ResizeElementName = "SHAPE", ResizeAdornmentTemplate = ResizeAdornments.Horizontal
            }
            .Bind(new Binding(nameof(Northwoods.Go.Node.Location), nameof(BusNodeData.Location)).MakeTwoWay())
            .Add(
                new Shape("Rectangle")
                    {
                        Name = "SHAPE", Cursor = "pointer", StrokeWidth = 0, Height = 10,
                        PortId = "", FromLinkable = true, ToLinkable = true
                    }
                    .Bind(new Binding(nameof(Shape.Fill), nameof(ElecDiagramTheme.BusLineBrush)).OfModel())
                    .Bind(new Binding(nameof(Shape.Width), nameof(BusNodeData.Width)).MakeTwoWay()),
                new Shape("Rectangle")
                    {
                        StrokeWidth = 0, Height = 4, Stretch = Stretch.Horizontal,
                        Alignment = Spot.Center
                    }
                    .Bind(new Binding(nameof(Shape.Fill), nameof(ElecDiagramTheme.BusBodyBrush)).OfModel()),
                new TextBlock
                    {
                        Alignment = Spot.Right, AlignmentFocus = new Spot(0, 0.5, -20, 0),
                    }
                    .Bind("Text", nameof(BusNodeData.Width), val => val.ToString())
                    .Bind(new Binding(nameof(TextBlock.Background), nameof(ElecDiagramTheme.DefaultTextBackBrush)).OfModel())
                    .Bind(new Binding(nameof(TextBlock.Stroke), nameof(ElecDiagramTheme.DefaultTextBrush)).OfModel())
                    .Bind(new Binding(nameof(TextBlock.Font), nameof(ElecDiagramTheme.DefaultFont)).OfModel()),
                new TextBlock
                    {
                        Alignment = Spot.TopLeft, AlignmentFocus = new Spot(0, 1, 0, 10),
                    }
                    .Bind("Text", nameof(BusNodeData.Location), val => val.ToString())
                    .Bind(new Binding(nameof(TextBlock.Background), nameof(ElecDiagramTheme.DefaultTextBackBrush)).OfModel())
                    .Bind(new Binding(nameof(TextBlock.Stroke), nameof(ElecDiagramTheme.DefaultTextBrush)).OfModel())
                    .Bind(new Binding(nameof(TextBlock.Font), nameof(ElecDiagramTheme.DefaultFont)).OfModel())
            );
    }
}