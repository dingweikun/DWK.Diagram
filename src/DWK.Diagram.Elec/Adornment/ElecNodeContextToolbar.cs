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
                GeometryString = iconString, DesiredSize = iconSize, StrokeWidth = 1.5, Stroke = stroke, Fill = "transparent"
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

    private static Panel ButtonSwitchOpen()
    {
        return MakeButton(IconGeometry.SwitchOpen, new Size(13, 20), "Open Switch", OpenSwitch, "red")
            .Bind(new Binding("Visible", "", CanOpenSwitch).OfElement());

        void OpenSwitch(InputEvent e, GraphObject obj)
        {
            if (obj.Part is not GoAdornment { AdornedPart: Northwoods.Go.Node { Data: SwitchNodeData data } }) return;
            if (!data.Opened)
                e.Diagram.Model.Commit(m => m.Set(data, nameof(SwitchNodeData.Opened), true), nameof(OpenSwitch));
        }

        object CanOpenSwitch(object val, object targetObj)
        {
            return val is GoAdornment { AdornedPart: Northwoods.Go.Node { Data: SwitchNodeData { Opened: false } } };
        }
    }

    private static Panel ButtonSwitchClose()
    {
        return MakeButton(IconGeometry.SwitchClose, new Size(13, 20), "Close Switch", CloseSwitch, "green")
            .Bind(new Binding("Visible", "", CanCloseSwitch).OfElement());

        void CloseSwitch(InputEvent e, GraphObject obj)
        {
            if (obj.Part is not GoAdornment { AdornedPart: Northwoods.Go.Node { Data: SwitchNodeData data } }) return;
            if (data.Opened)
                e.Diagram.Model.Commit(m => m.Set(data, nameof(SwitchNodeData.Opened), false), nameof(CloseSwitch));
        }

        object CanCloseSwitch(object val, object targetObj)
        {
            return val is GoAdornment { AdornedPart: Northwoods.Go.Node { Data: SwitchNodeData { Opened: true } } };
        }
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