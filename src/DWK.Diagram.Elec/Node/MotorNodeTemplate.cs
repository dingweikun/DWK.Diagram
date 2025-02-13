using DWK.Diagram.Adornment;
using DWK.Diagram.ElecModels;
using Northwoods.Go.Models;

namespace DWK.Diagram.Node;

internal class MotorNodeTemplate : ElecNodeTemplate<MotorNodeData>
{
    public const double DefaultWidth = 100;
    private const double MaxSize = 400;
    private const double ShapeSize1 = 80;
    private const double ShapeSize2 = 36;

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
                        Width = ShapeSize1, Height = ShapeSize1,
                        Fill = Color1, Stroke = Color2, StrokeWidth = 8
                    }
                    .Bind(BindingScale(nameof(Sample.ResizedWidth), DefaultWidth)),
                new Shape
                    {
                        Width = ShapeSize2, Height = ShapeSize2,
                        Fill = Color2, StrokeWidth = 0,
                        GeometryString =
                            "F M 8.3923339e-8,1075.287 H 19.300024 v -44.8399 c 0,-10.1579 -1.741356,-25.1046 -2.757146,-35.26247 h 0.580451 c 7.711934,23.76337 16.610317,47.01597 25.249656,70.37977 h 12.334601 c 8.779043,-23.306 17.116734,-46.7943 25.249655,-70.37977 h 0.725565 c -1.160903,10.15787 -2.757146,25.10457 -2.757146,35.26247 v 44.8399 H 97.51591 V 967.75828 H 73.717384 C 64.84767,990.67797 56.773927,1016.6971 49.338407,1038.7185 H 48.612842 C 41.475761,1014.8228 32.580843,991.94451 23.798526,967.75828 H 8.3923339e-8 Z"
                    }
                    .Bind(BindingScale(nameof(Sample.ResizedWidth), DefaultWidth))
            );
    }
}