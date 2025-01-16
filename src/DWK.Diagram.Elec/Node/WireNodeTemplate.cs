using System.ComponentModel;
using Northwoods.Go.Models;
using Northwoods.Go.Tools;
using GoPanel = Northwoods.Go.Panel;
using GoTextBlock = Northwoods.Go.TextBlock;
using GoAdornment = Northwoods.Go.Adornment;

namespace DWK.Diagram.Node;

public static class WireNodeTemplate
{
    public static Northwoods.Go.Node Make()
    {
        var icon =
            "M938.666667 970.666667H53.333333V85.333333c0-17.066667 14.933333-32 32-32s32 14.933333 32 32v821.333334H938.666667c17.066667 0 32 14.933333 32 32s-14.933333 32-32 32z M778.666667 746.666667h-64c0-57.6-10.666667-113.066667-34.133334-166.4-21.333333-53.333333-53.333333-100.266667-93.866666-142.933334-40.533333-40.533333-89.6-72.533333-142.933334-93.866666-53.333333-21.333333-108.8-34.133333-166.4-34.133334v-64c66.133333 0 130.133333 12.8 192 38.4 61.866667 25.6 115.2 61.866667 162.133334 108.8 46.933333 46.933333 83.2 102.4 108.8 162.133334 25.6 61.866667 38.4 125.866667 38.4 192z M181.333333 296.533333c-14.933333-8.533333-14.933333-27.733333 0-36.266666L277.333333 202.666667c14.933333-8.533333 32 2.133333 32 19.2v110.933333c0 17.066667-17.066667 27.733333-32 19.2l-96-55.466667zM765.866667 842.666667c-8.533333 14.933333-27.733333 14.933333-36.266667 0L672 746.666667c-8.533333-14.933333 2.133333-32 19.2-32h110.933333c17.066667 0 27.733333 17.066667 19.2 32l-55.466666 96z";


        // var ContextMenu = // define a context menu for each node that has one button
        //     Builder.Make<Adornment>("ContextMenu")
        //         .Add(
        //             Builder.Make<Panel>("ContextMenuButton")
        //                 .Set(new
        //                 {
        //                     ButtonBorder_Fill = "white",
        //                     _ButtonFillOver = "skyblue",
        //                     // Click = changeColor
        //                 })
        //                 .Add(
        //                     new TextBlock("Change Color")
        //                         .Bind(new Binding(nameof(TextBlock.Font), nameof(ElecDiagramTheme.DefaultFont)).OfModel())
        //                 )
        //         );


        // var commandsAdornment = Builder.Make<Adornment>("ContextMenu")
        var commandsAdornment = new GoAdornment(PanelLayoutHorizontal.Instance) { Padding = 16 }
            .Add(
                // new Panel("Auto")
                //   .Add(
                //     new Shape { Fill = null, Stroke = "red", StrokeWidth = 2, ShadowVisible = false },
                //     new Placeholder()
                //   ),
                MakeButton(IconGeometry.ShiftOrientation, "Shift Orientation", CommiteOperation.ShiftOrientation),
                Builder.Make<GoPanel>("Button")
                    .Add(
                        new Shape
                        {
                            GeometryString = icon, Height = 18, Width = 18, Margin = 4
                        }
                    )
                    .Set(new
                    {
                        Click = new Action<InputEvent, GraphObject>(CommiteOperation.ShiftOrientation),
                    }),
                Builder.Make<GoPanel>("Button")
                    .Add(
                        new Shape
                        {
                            GeometryString = icon, Height = 18, Width = 18, Margin = 4
                        }
                    )
                    .Set(new
                    {
                        Click = new Action<InputEvent, GraphObject>(CommiteOperation.ShiftOrientation),
                    }),
                Builder.Make<GoPanel>("Button")
                    .Add(
                        new Shape
                        {
                            GeometryString = icon, Height = 18, Width = 18, Margin = 4
                        }
                    )
                    .Set(new
                    {
                        Click = new Action<InputEvent, GraphObject>(CommiteOperation.ShiftOrientation),
                        ToolTip = Builder.Make<GoAdornment>("ToolTip")
                            .Add(
                                new GoTextBlock("Shift Orientation").Bind(new Binding("Font", nameof(ElecDiagramTheme.DefaultFont)).OfModel()))
                    })
            );


        //--------------------------------------------------------------------------------------------------------------------------------------------

        return new Northwoods.Go.Node(PanelLayoutSpot.Instance)
            {
                SelectionElementName = "SHAPE",
                LocationElementName = "SHAPE", LocationSpot = new Spot(0, 0.5),
                Resizable = true, ResizeElementName = "SHAPE", ResizeAdornmentTemplate = ResizeAdornments.Horizontal,
                // LinkValidation = LinkValidation,
                ContextMenu = commandsAdornment
            }
            .Bind(new Binding("Angle", nameof(WireNodeData.Angle)).MakeTwoWay())
            .Add(
                new Shape("Rectangle")
                    {
                        Name = "SHAPE", Height = 5, StrokeWidth = 0
                    }
                    .Bind(new Binding(nameof(Shape.Fill), nameof(ElecDiagramTheme.WireBrush)).OfModel())
                    .Bind(new Binding(nameof(Shape.Width), nameof(WireNodeData.Width)).MakeTwoWay()),
                new Shape("Rectangle")
                {
                    DesiredSize = new Size(7, 7), StrokeWidth = 0,
                    Alignment = Spot.Left, AlignmentFocus = Spot.Center,
                    PortId = "s",
                    FromLinkable = true, FromSpot = Spot.Left, FromLinkableDuplicates = true,
                    ToLinkable = true, ToSpot = Spot.Left, ToLinkableDuplicates = true,
                    Fill = "red"
                }
                // .Bind(new Binding(nameof(Shape.Fill), nameof(ElecDiagramTheme.PortBrush)).OfModel())
                ,
                new Shape("Rectangle")
                {
                    DesiredSize = new Size(7, 7), StrokeWidth = 0,
                    Alignment = Spot.Right, AlignmentFocus = Spot.Center,
                    PortId = "e",
                    FromLinkable = true, FromSpot = Spot.Right, FromLinkableDuplicates = true,
                    ToLinkable = true, ToSpot = Spot.Right, ToLinkableDuplicates = true,
                    Fill = "blue"
                }
                //.Bind(new Binding(nameof(Shape.Fill), nameof(ElecDiagramTheme.PortBrush)).OfModel())
            );

        GoPanel MakeButton(string iconString, string tip, Action<InputEvent, GraphObject> action)
        {
            return Builder.Make<GoPanel>("Button")
                .Add(new Shape { GeometryString = iconString, Height = 18, Width = 18, Margin = 4 })
                .Set(new { Click = action, ToolTip = MakeToolTip(tip) });
        }

        GoAdornment MakeToolTip(string tip)
        {
            return Builder.Make<GoAdornment>("ToolTip")
                .Add(new GoTextBlock(tip)
                    .Bind(new Binding("Font", nameof(ElecDiagramTheme.DefaultFont)).OfModel()));
        }


        bool LinkValidation(Northwoods.Go.Node formNode, GraphObject fromPort, Northwoods.Go.Node toNode, GraphObject toPort, Link link)
        {
            // if (formNode.Data is WireNodeData && formNode.FindLinksConnected(fromPort.PortId).Any())
            //     return false;
            // if (toNode.Data is WireNodeData && toNode.FindLinksConnected(toNode.PortId).Any())
            //     return false;

            if (formNode.Data is WireNodeData)
            {
                var links = formNode.FindLinksConnected(fromPort.PortId);
                if (links.Any()) return false;
            }

            if (toNode.Data is WireNodeData)
            {
                var links = toNode.FindLinksConnected(toNode.PortId);
                if (links.Any()) return false;
            }

            return true;
        }
    }
}

public static class CommiteOperation
{
    public static void ShiftOrientation(InputEvent e, GraphObject obj)
    {
        if (obj.Part is GoAdornment { AdornedPart: Northwoods.Go.Node { Data: IAngle data } })
        {
            var angle = data.Angle == 0 ? 90 : 0;
            e.Diagram.Model.Commit(m => m.Set(data, nameof(IAngle.Angle), angle), nameof(ShiftOrientation));
        }
    }
}