using DWK.Diagram.Adornment;
using DWK.Diagram.ElecModels;
using Northwoods.Go.Models;

namespace DWK.Diagram.Node;

internal class BreakerNodeTemplate : ElecNodeTemplate<BreakerNodeData>
{
    private static GoBrush Color1 { get; } = "green";
    private static GoBrush Color2 { get; } = "red";

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
                        GeometryString = "M-40,20 L-30,20 L-30 10 A 32.5 32.5 0 0 1 30 10 L30,20 L40,20",
                        Stroke = Color1, StrokeWidth = 6
                    }
                    .Bind(new Binding("Visible", nameof(SwitchNodeData.Opened), o => o is false)),
                new Shape
                    {
                        GeometryString = "M-40,20 L-30,20 M-30 10 A 32.5 32.5 0 0 1 30 10 M30,20 L40,20",
                        Stroke = Color2, StrokeWidth = 6
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