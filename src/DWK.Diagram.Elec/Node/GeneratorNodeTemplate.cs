using DWK.Diagram.Adornment;
using DWK.Diagram.ElecModels;
using Northwoods.Go.Models;

namespace DWK.Diagram.Node;

public class GeneratorNodeTemplate : ElecNodeTemplate<GeneratorNodeData>
{
    public const double DefaultWidth = 100;
    private const double MaxSize = 400;
    private const double ShapeSize1 = 80;

    private static GoBrush Color1 { get; } = new("red");
    private static GoBrush Color2 { get; } = new("white");

    public override Northwoods.Go.Node Make()
    {
        return new Northwoods.Go.Node(PanelLayoutSpot.Instance)
            {
                SelectionElementName = "SHAPE",
                LocationElementName = "SHAPE", LocationSpot = Spot.Center,
                ResizeElementName = "SHAPE", Resizable = true, ResizeAdornmentTemplate = ResizeAdornments.Uniform, ResizeCellSize = new Size(20, 20),
                LinkValidation = OneLinkValidation
            }
            .Bind(BindingLocation())
            .Add(
                new Shape("Rectangle")
                    {
                        Name = "SHAPE", Fill = "transparent", StrokeWidth = 0,
                        MinSize = new Size(DefaultWidth, DefaultWidth),
                        MaxSize = new Size(MaxSize, MaxSize)
                    }
                    .Bind(BindingUniformSize()),
                // Tag 显示
                TagTextBlock().Bind(BindingOrientation(true)),
                // 位置显示
                LocationTextBlock(),
                // 模块图案
                new Shape("Ellipse")
                    {
                        Stretch = Stretch.Fill, Fill = Color1, StrokeWidth = 0,
                        // 锚点属性
                        FromLinkable = false, ToLinkable = true, ToMaxLinks = 1
                    }
                    // 设置为连接锚点
                    .Set(ElecPortSetting("EP", EPortType.NODE, EPortType.LINK)),
                new Shape("Ellipse")
                    {
                        Width = ShapeSize1, Height = ShapeSize1, Fill = Color1, Stroke = Color2, StrokeWidth = 8
                    }
                    .Bind(BindingScale(nameof(Sample.ResizedWidth), DefaultWidth)),
                new Shape
                    {
                        Stroke = Color2, StrokeWidth = 8,
                        GeometryString = "M 0,0 L 0,-40 M 0,0 L 34.64,20 M 0,0 L -34.64,20 M0,40"
                    }
                    .Bind(BindingScale(nameof(Sample.ResizedWidth), DefaultWidth))
            );
    }
}