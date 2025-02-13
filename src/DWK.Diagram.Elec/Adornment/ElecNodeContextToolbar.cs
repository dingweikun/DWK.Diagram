using System.Runtime.CompilerServices;
using DWK.Diagram.Node;
using Northwoods.Go.Models;

namespace DWK.Diagram.Adornment;

using GoAdornment = Northwoods.Go.Adornment;

public static class ElecNodeContextToolbar
{
    public static GoAdornment? Make(string category) => category switch
    {
        ElecNodeCategory.Switch => MakeToolbar()
            .Add(ButtonShiftOrientation())
            .Add(ButtonSwitchOpen())
            .Add(ButtonSwitchClose()),

        ElecNodeCategory.Wire => MakeToolbar()
            .Add(ButtonShiftOrientation()),

        ElecNodeCategory.Line => MakeToolbar()
            .Add(ButtonShiftOrientation()),

        ElecNodeCategory.Breaker => MakeToolbar()
            .Add(ButtonShiftOrientation())
            .Add(ButtonBreakerOpen())
            .Add(ButtonBreakerClose()),

        ElecNodeCategory.Bus => MakeToolbar()
            .Add(ButtonBusToBusPoint()),

        ElecNodeCategory.BusPoint => MakeToolbar()
            .Add(ButtonBusPointToBus()),

        _ => null
    };

    #region common make function

    private static GoAdornment MakeToolbar() => new(PanelLayoutHorizontal.Instance)
    {
        Padding = 8,
    };

    private static Panel MakeButton(string iconString, Size iconSize, string tip, Action<InputEvent, GraphObject> action, string stroke = "black")
    {
        return Builder.Make<Panel>("Button")
            .Add(new Shape
            {
                GeometryString = iconString, DesiredSize = iconSize, StrokeWidth = 2, Stroke = stroke, Fill = "transparent"
            })
            .Set(new { Height = 36, Width = 36, Click = action, ToolTip = MakeToolTip(tip) });
    }

    private static GoAdornment MakeToolTip(string tip)
    {
        return Builder.Make<GoAdornment>("ToolTip")
            .Add(new TextBlock(tip)
                .Bind(new Binding("Font", nameof(ElecDiagramTheme.DefaultFont)).OfModel()));
    }

    #endregion

    #region tool button make function

    private static Panel ButtonShiftOrientation()
    {
        return MakeButton(IconGeometry.ShiftOrientation, new Size(20, 20), "Shift Orientation", ShiftOrientation);

        void ShiftOrientation(InputEvent e, GraphObject obj)
        {
            if (obj.Part is GoAdornment { AdornedPart: Northwoods.Go.Node { Data: IOrientation data } })
            {
                var vert = !data.IsVertical;
                e.Diagram.Model.Commit(m => m.Set(data, nameof(IOrientation.IsVertical), vert), nameof(ShiftOrientation));
            }
        }
    }

    #region toggle function

    private static void _toggle(InputEvent e, GraphObject obj)
    {
        if (obj.Part is not GoAdornment { AdornedPart: Northwoods.Go.Node { Data: IToggle toggle } }) return;

        var toState = !toggle.Opened;
        var commit = toState ? "toggle to open" : "toggle to close";

        e.Diagram.Model.Commit(m => m.Set(toggle, nameof(IToggle.Opened), toState), commit);
    }

    private static object _toggleCanOpen(object val, object targetObj) =>
        val is GoAdornment { AdornedPart: Northwoods.Go.Node { Data: IToggle { Opened: false } } };

    private static object _toggleCanClose(object val, object targetObj) =>
        val is GoAdornment { AdornedPart: Northwoods.Go.Node { Data: IToggle { Opened: true } } };

    #endregion

    private static Panel ButtonBreakerOpen()
    {
        return MakeButton(IconGeometry.BreakerOpen, new Size(10, 22), "Open Breaker", _toggle, "red")
            .Bind(new Binding("Visible", "", _toggleCanOpen).OfElement());
    }

    private static Panel ButtonBreakerClose()
    {
        return MakeButton(IconGeometry.BreakerClose, new Size(10, 22), "Close Breaker", _toggle, "green")
            .Bind(new Binding("Visible", "", _toggleCanClose).OfElement());
    }

    private static Panel ButtonSwitchOpen()
    {
        return MakeButton(IconGeometry.SwitchOpen, new Size(13, 22), "Open Switch", _toggle, "red")
            .Bind(new Binding("Visible", "", _toggleCanOpen).OfElement());
    }

    private static Panel ButtonSwitchClose()
    {
        return MakeButton(IconGeometry.SwitchClose, new Size(13, 22), "Close Switch", _toggle, "green")
            .Bind(new Binding("Visible", "", _toggleCanClose).OfElement());
    }

    private static Panel ButtonBusToBusPoint()
    {
        return MakeButton(IconGeometry.BusPoint, new Size(16, 16), "Turn To BusPoint", BusToBusPoint, "blue");

        void BusToBusPoint(InputEvent e, GraphObject obj)
        {
            if (obj.Part is not GoAdornment { AdornedPart: Northwoods.Go.Node { Data: BusNodeData currentNodeData } }) return;

            if (e.Diagram.Model is not ElecModel em) return;

            var fromlinks = em.LinkDataSource.Where(link => link.From == currentNodeData.Key).ToArray();
            var tolinks = em.LinkDataSource.Where(link => link.To == currentNodeData.Key).ToArray();

            e.Diagram.Model.Commit(m =>
            {
                // TODO: 完全复制模型数据
                var newNodeData = new BusPointNodeData()
                {
                    Location = currentNodeData.Location
                };

                em.AddNodeData(newNodeData);

                foreach (var link in fromlinks)
                {
                    em.SetFromKeyForLinkData(link, newNodeData.Key);
                }

                foreach (var link in tolinks)
                {
                    em.SetToKeyForLinkData(link, newNodeData.Key);
                }

                em.RemoveNodeData(currentNodeData);
            }, nameof(BusToBusPoint));
        }
    }

    private static Panel ButtonBusPointToBus()
    {
        return MakeButton(IconGeometry.Bus, new Size(20, double.NaN), "Turn To Bus", BusPointToBus, "red");

        void BusPointToBus(InputEvent e, GraphObject obj)
        {
            if (obj.Part is not GoAdornment { AdornedPart: Northwoods.Go.Node { Data: BusPointNodeData currentNodeData } }) return;

            if (e.Diagram.Model is not ElecModel em) return;

            var fromlinks = em.LinkDataSource.Where(link => link.From == currentNodeData.Key).ToArray();
            var tolinks = em.LinkDataSource.Where(link => link.To == currentNodeData.Key).ToArray();

            e.Diagram.Model.Commit(m =>
            {
                // TODO: 完全复制模型数据
                var newNodeData = new BusNodeData()
                {
                    Location = currentNodeData.Location
                };

                em.AddNodeData(newNodeData);

                foreach (var link in fromlinks)
                {
                    em.SetFromKeyForLinkData(link, newNodeData.Key);
                }

                foreach (var link in tolinks)
                {
                    em.SetToKeyForLinkData(link, newNodeData.Key);
                }

                em.RemoveNodeData(currentNodeData);
            }, nameof(BusPointToBus));
        }
    }

    #endregion
}