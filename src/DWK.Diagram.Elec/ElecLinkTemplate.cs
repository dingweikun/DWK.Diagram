using DWK.Diagram.Node;
using Northwoods.Go.Models;

namespace DWK.Diagram;

public static class ElecLinkTemplate
{
    public static Link Make()
    {
        return new Link
            {
                Corner = 6,
                ToShortLength = 3,
                Routing = LinkRouting.AvoidsNodes, // LinkRouting.AvoidsNodes,
                Reshapable = true, Resegmentable = true,
                RelinkableFrom = true, RelinkableTo = true,
                // MouseEnter = (e, l, _) => { ((l as Link).Elt(2) as Shape).Stroke = "rgba(0,90,156,.3)"; },
                // MouseLeave = (e, l, _) => { ((l as Link).Elt(2) as Shape).Stroke = "transparent"; }
            }
            .Bind(new Binding("Points").MakeTwoWay())
            .Add(
                new Shape { ToArrow = "Standard", Stroke = null }
                    .Bind(new Binding(nameof(Shape.Fill), nameof(ElecDiagramTheme.LinkBrush)).OfModel()),
                new Shape { IsPanelMain = true, StrokeWidth = 2 }
                    .Bind(new Binding(nameof(Shape.Stroke), nameof(ElecDiagramTheme.LinkBrush)).OfModel()),
                new Shape { IsPanelMain = true, Stroke = "transparent", StrokeWidth = 6 }
            );
    }
}