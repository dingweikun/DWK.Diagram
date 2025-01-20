using Northwoods.Go.Models;

namespace DWK.Diagram.Node;

internal class TransfNodeTemplate : ElecNodeTemplate<TransfNodeData>
{
    public override Northwoods.Go.Node Make()
    {
        var brush = new GoBrush("red");

        return new Northwoods.Go.Node(PanelLayoutSpot.Instance)
            {
                SelectionElementName = "SHAPE",
                LocationElementName = "SHAPE", LocationSpot = Spot.Center,
            }
            .Bind(BindingLocation())
            .Add(
                new Shape("Rectangle")
                {
                    Name = "SHAPE", Width = 60, Height = 100, Fill = "transparent", StrokeWidth = 0
                },
                // Tag 显示
                TagTextBlock(),
                // 位置显示
                LocationTextBlock(),
                // 模块图案
                new Shape
                {
                    GeometryString = "F M 0,-30 a 30,30 0 1,1 0,60 a 30,30 0 1,1 0,-60 M 0,-25 a 25,25 0 1,0 0,50 a 25,25 0 1,0 0,-50",
                    StrokeWidth = 0, Fill = brush,
                    Alignment = Spot.Top, AlignmentFocus = Spot.Top
                },
                new Shape
                {
                    GeometryString = "F M 0,-30 a 30,30 0 1,1 0,60 a 30,30 0 1,1 0,-60 M 0,-25 a 25,25 0 1,0 0,50 a 25,25 0 1,0 0,-50",
                    StrokeWidth = 0, Fill = brush,
                    Alignment = Spot.Bottom, AlignmentFocus = Spot.Bottom
                },
                // new Shape
                // {
                //     GeometryString = "M0,0 L0,100 M10,10 L0,0 L-10,10",
                //     StrokeWidth = 4, Stroke = brush, Angle = 70
                // }

                // 连接锚点
                new Shape("Rectangle")
                    {
                        Height = 8, Width = 8, StrokeWidth = 0, Alignment = Spot.Top, AlignmentFocus = Spot.Center
                    }
                    .Bind(new Binding("Fill", nameof(ElecDiagramTheme.PortBrush)).OfModel()),
                new Shape("Rectangle")
                    {
                        Height = 8, Width = 8, StrokeWidth = 0, Alignment = Spot.Bottom, AlignmentFocus = Spot.Center
                    }
                    .Bind(new Binding("Fill", nameof(ElecDiagramTheme.PortBrush)).OfModel())
            );
    }
}