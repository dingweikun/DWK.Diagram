using DWK.Diagram.Node;
using Northwoods.Go.Models;

namespace DWK.Diagram.Adornment;

using GoAdornment = Northwoods.Go.Adornment;

public static class ElecNodeContextToolbar
{
    public static GoAdornment Make(string category) => category switch
    {
        ElecNodeCategory.Switch => MakeToolbar()
            .Add(ButtonShiftOrientation())
            .Add(ButtonSwitchOpen())
            .Add(ButtonSwitchClose()),

        _ => throw new ArgumentOutOfRangeException(nameof(category))
    };

    #region common make function

    private static GoAdornment MakeToolbar() => new GoAdornment(PanelLayoutHorizontal.Instance) { Padding = 16 };

    private static Panel MakeButton(string iconString, string tip, Action<InputEvent, GraphObject> action)
    {
        return Builder.Make<Panel>("Button")
            .Add(new Shape
            {
                GeometryString = iconString,  Stroke = "black", Fill = "black",
                Stretch = Stretch.Fill
            })
            .Set(new {  Height = 40, Width = 40, Padding=8, Click = action, ToolTip = MakeToolTip(tip) });
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
        return MakeButton(IconGeometry.ShiftOrientation, "Shift Orientation", ShiftOrientation);

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
        return MakeButton(IconGeometry.SwitchOpen, "Open Switch", OpenSwitch);

        void OpenSwitch(InputEvent e, GraphObject obj)
        {
            if (obj.Part is not GoAdornment { AdornedPart: Northwoods.Go.Node { Data: SwitchNodeData data } }) return;
            if (data.Opened is false)
                e.Diagram.Model.Commit(m => m.Set(data, nameof(SwitchNodeData.Opened), true), nameof(OpenSwitch));
        }
    }

    private static Panel ButtonSwitchClose()
    {
        return MakeButton(IconGeometry.SwitchClose, "Close Switch", CloseSwitch);

        void CloseSwitch(InputEvent e, GraphObject obj)
        {
            if (obj.Part is not GoAdornment { AdornedPart: Northwoods.Go.Node { Data: SwitchNodeData data } }) return;
            if (data.Opened)
                e.Diagram.Model.Commit(m => m.Set(data, nameof(IOrientation.IsVertical), false), nameof(CloseSwitch));
        }
    }

    #endregion
}