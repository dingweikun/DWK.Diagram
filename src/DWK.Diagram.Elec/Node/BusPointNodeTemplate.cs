using DWK.Diagram.Adornment;
using DWK.Diagram.ElecModels;

namespace DWK.Diagram.Node;

public class BusPointNodeTemplate : ElecNodeTemplate<BusPointNodeData>
{
    public override Northwoods.Go.Node Make()
    {
        return new Northwoods.Go.Node(PanelLayoutSpot.Instance)
            {
                SelectionElementName = "SHAPE",
                LocationElementName = "SHAPE", LocationSpot = Spot.Left,
                ContextMenu = ElecNodeContextToolbar.Make(Sample.Category)
            }
            .Bind(BindingLocation())
            .Add(
                new Shape("Rectangle")
                {
                    Name = "SHAPE", Fill = "lightblue", StrokeWidth = 0,
                    Width = 20, Height = 20,
                },
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