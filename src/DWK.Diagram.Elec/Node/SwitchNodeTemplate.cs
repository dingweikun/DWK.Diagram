using DWK.Diagram.Adornment;
using DWK.Diagram.ElecModels;
using Northwoods.Go.Models;

namespace DWK.Diagram.Node;

internal class SwitchNodeTemplate : ElecNodeTemplate<SwitchNodeData>
{
    public override Northwoods.Go.Node Make()
    {
        return new Northwoods.Go.Node(PanelLayoutSpot.Instance)
            {
                SelectionElementName = "SHAPE",
                LocationElementName = "SHAPE", LocationSpot = Spot.BottomLeft,
                ContextMenu = ElecNodeContextToolbar.Make(Sample.Category),
                LinkValidation = OneLinkValidation
            }
            .Bind(BindingLocation())
            .Bind(BindingOrientation())
            .Add(
                new Shape("Rectangle")
                    { Name = "SHAPE", Fill = "transparent", Width = 80, Height = 30, StrokeWidth = 0 },
                // Tag 显示
                TagTextBlock().Bind(BindingOrientation(true)),
                // 位置显示
                LocationTextBlock(),
                // 模块图案
                new Shape
                    {
                        Stroke = "green", StrokeWidth = 6, StrokeJoin = LineJoin.Bevel,
                        GeometryString = "M0,0 L20,0 L20,-30 M80,0 L70,0 L10,-20"
                    }
                    .Bind(new Binding("Visible", nameof(SwitchNodeData.Opened), o => o is false)),
                new Shape
                    {
                        Stroke = "red", StrokeWidth = 6, StrokeJoin = LineJoin.Bevel,
                        GeometryString = "M0,0 L20,0 M80,0 L70,0 L20,-30"
                    }
                    .Bind(new Binding("Visible", nameof(SwitchNodeData.Opened), o => o is true)),
                // 连接锚点
                DefaultPortShape()
                    .Set(ElecPortSetting("EIN", EPortType.LINK, EPortType.NODE))
                    .Set(new
                    {
                        Alignment = Spot.BottomLeft,
                        ToLinkable = true, ToMaxLinks = 1, ToSpot = Spot.Left,
                        FromLinkable = true, FromMaxLinks = 1, FromSpot = Spot.Left
                    }),
                DefaultPortShape()
                    .Set(ElecPortSetting("EOUT", EPortType.LINK, EPortType.NODE))
                    .Set(new
                    {
                        Alignment = Spot.BottomRight,
                        ToLinkable = true, ToMaxLinks = 1, ToSpot = Spot.Right,
                        FromLinkable = true, FromMaxLinks = 1, FromSpot = Spot.Right
                    })
            );
    }
}