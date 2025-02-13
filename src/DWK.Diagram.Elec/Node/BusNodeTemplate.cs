using DWK.Diagram.Adornment;
using DWK.Diagram.ElecModels;
using Northwoods.Go.Models;

namespace DWK.Diagram.Node;

internal class BusNodeTemplate : ElecNodeTemplate<BusNodeData>
{
    public const double DefaultWidth = 200;

    private static GoBrush Color1 { get; } = new("red");
    private static GoBrush Color2 { get; } = new("lightyellow");

    public override Northwoods.Go.Node Make()
    {
        return new Northwoods.Go.Node(PanelLayoutSpot.Instance)
            {
                SelectionElementName = "SHAPE",
                LocationElementName = "SHAPE", LocationSpot = Spot.Left,
                ResizeElementName = "SHAPE", Resizable = true, ResizeAdornmentTemplate = ResizeAdornments.Horizontal,
                ResizeCellSize = new Size(10, double.NaN),
                ContextMenu = ElecNodeContextToolbar.Make(Sample.Category)
            }
            .Bind(BindingLocation())
            .Add(
                new Shape("Rectangle")
                    {
                        Name = "SHAPE", Fill = Color2, StrokeWidth = 0,
                        Height = 20, MinSize = new Size(100, double.NaN)
                    }
                    .Bind(new Binding(nameof(Shape.Width), nameof(Sample.ResizedWidth)).MakeTwoWay()),
                // 模块图案
                new Shape
                    {
                        GeometryString = "M0,0 L 200,0 M0,18 L 200,18",
                        Stroke = Color1, StrokeWidth = 6, StrokeCap = LineCap.Square, Stretch = Stretch.Fill,

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
                    .Bind("Text", nameof(Sample.ResizedWidth), val => val.ToString())
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