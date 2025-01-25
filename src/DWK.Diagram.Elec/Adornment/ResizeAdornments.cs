using DWK.Diagram.Node;
using Northwoods.Go.Models;
using GoAdornment = Northwoods.Go.Adornment;

namespace DWK.Diagram.Adornment;

internal class ResizeAdornments
{
    public static GoAdornment Horizontal { get; } = MakeHorizontalResizeAdornment();

    public static GoAdornment Uniform { get; } = MakeUniformResizeAdornment();

    private static GoAdornment MakeHorizontalResizeAdornment()
    {
        return new GoAdornment(PanelLayoutSpot.Instance)
            .Add(
                new Placeholder(),
                new Shape
                    {
                        // left resize handle
                        Alignment = Spot.Left, Cursor = "col-resize",
                        DesiredSize = new Size(6, 6),
                    }
                    .Bind(new Binding(nameof(Shape.Fill), nameof(ElecDiagramTheme.AdornmentFillBrush)).OfModel())
                    .Bind(new Binding(nameof(Shape.Stroke), nameof(ElecDiagramTheme.AdornmentStrokeBrush)).OfModel()),
                new Shape
                    {
                        // right resize handle
                        Alignment = Spot.Right, Cursor = "col-resize",
                        DesiredSize = new Size(6, 6),
                    }
                    .Bind(new Binding(nameof(Shape.Fill), nameof(ElecDiagramTheme.AdornmentFillBrush)).OfModel())
                    .Bind(new Binding(nameof(Shape.Stroke), nameof(ElecDiagramTheme.AdornmentStrokeBrush)).OfModel())
            );
    }

    private static GoAdornment MakeUniformResizeAdornment()
    {
        return new GoAdornment(PanelLayoutSpot.Instance)
            .Add(
                new Placeholder(),

                //     new Shape
                //         {
                //             // left resize handle
                //             Alignment = Spot.Left, Cursor = "col-resize",
                //             DesiredSize = new Size(6, 6),
                //         }
                //         .Bind(new Binding(nameof(Shape.Fill), nameof(ElecDiagramTheme.AdornmentFillBrush)).OfModel())
                //         .Bind(new Binding(nameof(Shape.Stroke), nameof(ElecDiagramTheme.AdornmentStrokeBrush)).OfModel()),
                //     new Shape
                //         {
                //             // right resize handle
                //             Alignment = Spot.Right, Cursor = "col-resize",
                //             DesiredSize = new Size(6, 6),
                //         }
                //         .Bind(new Binding(nameof(Shape.Fill), nameof(ElecDiagramTheme.AdornmentFillBrush)).OfModel())
                //         .Bind(new Binding(nameof(Shape.Stroke), nameof(ElecDiagramTheme.AdornmentStrokeBrush)).OfModel())
                new Shape
                    {
                        // bottom-right resize handle
                        Alignment = Spot.BottomRight, Cursor = "ne-resize",
                        DesiredSize = new Size(6, 6),
                    }
                    .Bind(new Binding(nameof(Shape.Fill), nameof(ElecDiagramTheme.AdornmentFillBrush)).OfModel())
                    .Bind(new Binding(nameof(Shape.Stroke), nameof(ElecDiagramTheme.AdornmentStrokeBrush)).OfModel())
            );
    }
}