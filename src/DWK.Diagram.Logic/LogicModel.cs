using Avalonia.Media;
using Northwoods.Go;
using Northwoods.Go.Models;
using Northwoods.Go.PanelLayouts;
using FontWeight = Northwoods.Go.FontWeight;
using Stretch = Northwoods.Go.Stretch;

namespace DWK.Diagram;

// public class LogicModel : GraphLinksModel<LogicNodeData, Guid, object, LogicLinkData, Guid, string>
public class LogicModel : GraphLinksModel<LogicNodeData, int, object, LogicLinkData, string, string>
{
    public LogicModel()
    {
        LinkFromPortIdProperty = nameof(LogicLinkData.FromPort);
        LinkToPortIdProperty = nameof(LogicLinkData.ToPort);
    }
}

public static class LogicDiagram
{
    public const string FontName = "Noto Sans CJK SC";
    public const string Background = "lightBlue";
    public const string Tag = "MOOOOOOOOOOOOOOOOOOOOOOOOOOOO.";
    public static Font Font { get; } = new Font(FontName, 15, FontWeight.Bold);

    public static Font TagFont { get; } = new Font(FontName, 15, FontWeight.Regular);
    public static Font HeadFont { get; } = new Font(FontName, 15, FontWeight.Bold);
    public static Font PortFont { get; } = new Font(FontName, 13, FontWeight.Regular);

    public static Part MakeNodeTemplate()
        // public static Part MakeNodeTemplate(LogicSharedData sharedData)
    {
        var template = new Node(PanelLayoutVertical.Instance)
        {
            Background = Background, Padding = 5
        };

        template.Add(MakeTable());

        template.Add(new TextBlock(Tag) { Background = "Yellow", Font = TagFont });

        return template;

        Panel MakeTable()
        {
            var table = new Panel(PanelLayoutTable.Instance)
            {
                DefaultRowSeparatorStroke = "red",
                DefaultColumnSeparatorStroke = "red",
                Background = "blue",
                Padding = 5
            };

            table.Add(new RowDefinition() { Row = 0, MinHeight = 10 });
            table.Add(new RowDefinition() { Row = 1, MinHeight = 10 });
            table.Add(new RowDefinition() { Row = 2, MinHeight = 10 });

            // table.Add(new Shape("Rectangle")
            // {
            //     Fill = "Orange",
            //     Row = 0,
            //     RowSpan = 4,
            //     Column = 0,
            //     // ColumnSpan = 1,
            //     // MinSize = new Size(10, 10),
            //     Height = double.NaN, Width =double.NaN,
            // });

            // table.Add(new TextBlock("Header")
            // {
            //     Font = HeadFont,
            //     Margin = new Margin(8, 4),
            //     Row = 0,
            //     Column = 0
            // });


            return table;
        }
    }

    public static Part MakeNodeTemplate(string[] imports, string[] exports)
    {
        var portRows = int.Max(imports.Length, exports.Length);
        var iPos = (portRows - imports.Length) / 2;
        var oPos = (portRows - exports.Length) / 2;

        const string textBackground = "transparent";

        var table = new Part(PanelLayoutTable.Instance)
            .Add(new RowDefinition { Row = portRows + 1, Height = 20 })
            .Add(new Shape("rectangle")
            {
                Row = 0, RowSpan = portRows + 2,
                Column = 1, ColumnSpan = 2,
                Fill = "lightYellow",
                Stroke = "black",
                StrokeWidth = 1.5,
                Stretch = Stretch.Fill
            });

        table.Add(new TextBlock("Header")
        {
            Background = textBackground,
            Row = 0, Column = 1, ColumnSpan = 2,
            Stretch = Stretch.Horizontal,
            TextAlign = TextAlign.Center,
            Font = HeadFont,
            Margin = new Margin(4, 0)
        });

        foreach (var port in imports)
        {
            iPos += 1;
            table.Add(new Shape("rectangle")
            {
                Row = iPos, Column = 0,
                Height = 6, Width = 6,
                Alignment = Spot.Right,
                PortId = port,
                ToSpot = Spot.Left,
                ToLinkable = true,
                ToMaxLinks = 1
            });

            table.Add(new TextBlock(port)
            {
                Background = textBackground,
                Row = iPos, Column = 1,
                Font = PortFont,
                Margin = new Margin(0, 12, 0, 4),
                Alignment = Spot.Left,
                Cursor = "pointer"
            });
        }

        foreach (var port in exports)
        {
            oPos += 1;
            table.Add(new Shape("rectangle")
            {
                Row = oPos, Column = 3,
                Height = 6, Width = 6,
                Alignment = Spot.Left,
                PortId = port,
                FromSpot = Spot.Right,
                FromLinkable = true
            });

            table.Add(new TextBlock(port)
            {
                Background = textBackground,
                Row = oPos, Column = 2,
                Font = PortFont,
                Margin = new Margin(0, 4, 0, 12),
                Alignment = Spot.Right,
                Cursor = "pointer"
            });
        }

        table.Add(new TextBlock(Tag)
        {
            Row = portRows + 2,
            Column = 0, ColumnSpan = 4,
            Font = TagFont,
            TextAlign = TextAlign.Center,
            Stretch = Stretch.Horizontal
        });

        return table;
    }

    public static Link MakeLinkTemplate()
    {
        return new Link { Routing = LinkRouting.Orthogonal, Corner = 3 }
            .Add(
                new Shape(),
                new Shape { ToArrow = "Standard" }
            );
    }
}