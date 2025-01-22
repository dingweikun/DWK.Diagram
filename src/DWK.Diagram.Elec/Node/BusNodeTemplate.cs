using DWK.Diagram.Adornment;
using DWK.Diagram.ElecModels;
using Northwoods.Go.Models;

namespace DWK.Diagram.Node;

public class BusNodeTemplate : ElecNodeTemplate<BusNodeData>
{
    public override Northwoods.Go.Node Make()
    {
        return new Northwoods.Go.Node(PanelLayoutSpot.Instance)
            {
                SelectionElementName = "SHAPE",
                LocationElementName = "SHAPE", LocationSpot = Spot.Left,
                Resizable = true, ResizeElementName = "SHAPE", ResizeAdornmentTemplate = ResizeAdornments.Horizontal,
                ContextMenu = ElecNodeContextToolbar.Make(Sample.Category)
            }
            .Bind(BindingLocation())
            .Add(
                new Shape("Rectangle")
                    {
                        Name = "SHAPE", Fill = "lightyellow", StrokeWidth = 0,
                        Width = 200, Height = 20, MinSize = new Size(100, double.NaN)
                    }
                    .Bind(new Binding(nameof(Shape.Width), nameof(Sample.Width)).MakeTwoWay()),
                // 模块图案
                new Shape
                    {
                        GeometryString = "M0,0 L 200,0 M0,18 L 200,18",
                        Stroke = "red", StrokeWidth = 6, StrokeCap = LineCap.Square, Stretch = Stretch.Fill,

                        // 锚点属性
                        FromLinkable = true, FromSpot = Spot.TopBottomSides,
                        ToLinkable = true, ToSpot = Spot.TopBottomSides,
                    }
                    // 设置为连接锚点
                    .Set(ElecPortSetting("EP", EPortType.NODE, EPortType.NODE | EPortType.LINK)),
                // Tag 显示
                TagTextBlock(),
                new TextBlock
                    {
                        Alignment = Spot.Right, AlignmentFocus = new Spot(0, 0.5, -20, 0),
                    }
                    .Bind("Text", nameof(Sample.Width), val => val.ToString())
                    .Bind(new Binding(nameof(TextBlock.Background), nameof(ElecDiagramTheme.DefaultTextBackBrush)).OfModel())
                    .Bind(new Binding(nameof(TextBlock.Stroke), nameof(ElecDiagramTheme.DefaultTextBrush)).OfModel())
                    .Bind(new Binding(nameof(TextBlock.Font), nameof(ElecDiagramTheme.DefaultFont)).OfModel()),
                new TextBlock
                    {
                        Alignment = Spot.TopLeft, AlignmentFocus = new Spot(0, 1, 0, 10),
                    }
                    .Bind("Text", nameof(Sample.Location), val => val.ToString())
                    .Bind(new Binding(nameof(TextBlock.Background), nameof(ElecDiagramTheme.DefaultTextBackBrush)).OfModel())
                    .Bind(new Binding(nameof(TextBlock.Stroke), nameof(ElecDiagramTheme.DefaultTextBrush)).OfModel())
                    .Bind(new Binding(nameof(TextBlock.Font), nameof(ElecDiagramTheme.DefaultFont)).OfModel())
            );
    }
}