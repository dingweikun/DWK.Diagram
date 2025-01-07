using Avalonia.Controls.Shapes;
using Northwoods.Go.Models;
using Shape = Northwoods.Go.Shape;

namespace DWK.Diagram.Node;

public static class BusNodeTemplate
{
    public static Northwoods.Go.Node Make(ElecDiagramTheme? theme = null)
    {
        theme ??= new ElecDiagramTheme();

        var resizeAdornment = new Adornment(PanelLayoutSpot.Instance)
            .Add(
                new Placeholder(),
                new Shape
                {
                    // left resize handle
                    Alignment = Spot.Left, Cursor = "col-resize",
                    DesiredSize = new Size(6, 6),
                    Fill = theme.AdornmentFillBrush, Stroke = theme.AdornmentStrokeBrush
                },
                new Shape
                {
                    // right resize handle
                    Alignment = Spot.Right, Cursor = "col-resize",
                    DesiredSize = new Size(6, 6),
                    Fill = theme.AdornmentFillBrush, Stroke = theme.AdornmentStrokeBrush
                }
            );


        return new Northwoods.Go.Node(PanelLayoutAuto.Instance)
            {
                // LocationSpot = Spot.Center,
                // Padding = new Margin(0),

                // special resizing: just at the ends
                Resizable = true, ResizeAdornmentTemplate = resizeAdornment,
                // FromLinkable = true, ToLinkable = true
                MinSize = new Size(100, double.NaN),
                Background = "lightgray",
                // Height = 20
            }
            .Bind(new Binding(nameof(Northwoods.Go.Node.Location), nameof(BusNodeData.Location)).MakeTwoWay())
            .Bind(new Binding(nameof(Northwoods.Go.Node.Width), nameof(BusNodeData.Length)).MakeTwoWay())
            .Add(
                // new Shape("Rectangle")
                // {
                //     Fill = "red", StrokeWidth = 0,
                //     Height = 10, Stretch = Stretch.Horizontal,
                //     PortId = "",
                //     FromLinkable = true, ToLinkable = true
                // },
                new TextBlock()
                {
                    Font = theme.DefaultFont
                }.Bind("Text", nameof(BusNodeData.Length), val => val.ToString()),
                new Shape("Rectangle")
                {
                    Fill = "yellow", StrokeWidth = 0,
                    Height = 6, Stretch = Stretch.Horizontal,
                }
            );
    }
}