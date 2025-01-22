using DWK.Diagram.ElecModels;

namespace DWK.Diagram.Node;

public class LoadNodeTemplate : ElecNodeTemplate<LoadNodeData>
{
    public override Northwoods.Go.Node Make()
    {
        return new Northwoods.Go.Node(PanelLayoutSpot.Instance)
            {
                SelectionElementName = "SHAPE",
                LocationElementName = "SHAPE", LocationSpot = Spot.Center,
                LinkValidation = OneLinkValidation
            }
            .Bind(BindingLocation())
            .Add(
                new Shape("Rectangle")
                {
                    Name = "SHAPE", Fill = "transparent", Width = 100, Height = 100, StrokeWidth = 0, //Background = "yellow",
                },
                // Tag 显示
                TagTextBlock().Bind(BindingOrientation(true)),
                // 位置显示
                LocationTextBlock(),
                // 模块图案
                new Shape("Ellipse")
                    {
                        Width = 100, Height = 100, Fill = "red", StrokeWidth = 0,

                        // 锚点属性
                        FromLinkable = false, ToLinkable = true, ToMaxLinks = 1
                    }
                    // 设置为连接锚点
                    .Set(ElecPortSetting("EP", EPortType.NODE, EPortType.LINK)),
                new Shape("Ellipse") { Width = 80, Height = 80, Fill = "red", Stroke = "white", StrokeWidth = 8 }
            );
    }
}