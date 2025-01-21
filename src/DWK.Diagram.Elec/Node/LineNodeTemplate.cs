using DWK.Diagram.Adornment;
using DWK.Diagram.ElecModels;
using Shape = Northwoods.Go.Shape;

namespace DWK.Diagram.Node;

public class LineNodeTemplate : ElecNodeTemplate<LineNodeData>
{
    public override Northwoods.Go.Node Make()
    {
        return new Northwoods.Go.Node(PanelLayoutSpot.Instance)
            {
                SelectionElementName = "SHAPE",
                LocationElementName = "SHAPE", LocationSpot = Spot.Left,
                ContextMenu = ElecNodeContextToolbar.Make(Sample.Category),
                LinkValidation = OneLinkValidation<LineNodeData>
            }
            .Bind(BindingLocation())
            .Bind(BindingOrientation())
            .Add(
                new Shape("Rectangle")
                {
                    Name = "SHAPE", Fill = "transparent", Width = 200, Height = 40, StrokeWidth = 0, //Background = "yellow",
                },
                // 模块图案
                new Shape
                {
                    Stroke = "green", StrokeWidth = 6, StrokeJoin = LineJoin.Bevel,
                    GeometryString = "M0,0 L20,0 L30,20 L50,-20 L70,20 L90,-20 L110,20 L130,-20 L150,20 L170,-20 L180,0 L200,0"
                },
                // Tag 显示
                TagTextBlock().Bind(BindingOrientation(true)),
                // 位置显示
                LocationTextBlock(),
                // 连接锚点
                DefaultPortShape()
                    .Set(ElecPortSetting("EIN", EPortType.LINK, EPortType.NODE))
                    .Set(new
                    {
                        Alignment = Spot.Left,
                        ToLinkable = true, ToMaxLinks = 1, ToSpot = Spot.Left,
                        FromLinkable = true, FromMaxLinks = 1, FromSpot = Spot.Left
                    }),
                DefaultPortShape()
                    .Set(ElecPortSetting("EOUT", EPortType.LINK, EPortType.NODE))
                    .Set(new
                    {
                        Alignment = Spot.Right,
                        ToLinkable = true, ToMaxLinks = 1, ToSpot = Spot.Right,
                        FromLinkable = true, FromMaxLinks = 1, FromSpot = Spot.Right
                    })
            );
    }
}