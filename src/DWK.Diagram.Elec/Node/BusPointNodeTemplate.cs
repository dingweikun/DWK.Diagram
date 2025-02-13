using DWK.Diagram.Adornment;
using DWK.Diagram.ElecModels;
using Northwoods.Go.Models;

namespace DWK.Diagram.Node;

internal class BusPointNodeTemplate : ElecNodeTemplate<BusPointNodeData>
{
    public const double DefaultWidth = 20;
    
    public override Northwoods.Go.Node Make()
    {
        return new Northwoods.Go.Node(PanelLayoutSpot.Instance)
            {
                SelectionElementName = "SHAPE",
                LocationElementName = "SHAPE", LocationSpot = Spot.Center,
                Resizable = true, ResizeElementName = "SHAPE", ResizeAdornmentTemplate = ResizeAdornments.Uniform, ResizeCellSize = new Size(20, 20),
                ContextMenu = ElecNodeContextToolbar.Make(Sample.Category)
            }
            .Bind(BindingLocation())
            .Add(
                new Shape("Rectangle")
                    {
                        Name = "SHAPE", Fill = "lightblue", StrokeWidth = 0,
                        MinSize = new Size(20, double.NaN), MaxSize = new Size(40, double.NaN),
                    }
                    .Bind(BindingUniformSize()),
                // 模块图案
                new Shape
                    {
                        GeometryString = "M0,0 L 1,0 L 1,1 L 0,1 Z",
                        Stroke = "blue", StrokeWidth = 2, StrokeCap = LineCap.Square, Stretch = Stretch.Fill,

                        // 锚点属性
                        FromLinkable = true, FromSpot = Spot.AllSides,
                        ToLinkable = true, ToSpot = Spot.AllSides,
                    }
                    // 设置为连接锚点
                    .Set(ElecPortSetting("EP", EPortType.NODE, EPortType.NODE | EPortType.LINK)),
                // Tag 显示
                TagTextBlock()
            );
    }
}