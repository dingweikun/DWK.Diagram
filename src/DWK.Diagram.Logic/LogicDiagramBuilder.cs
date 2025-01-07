using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia.Controls;
using Northwoods.Go;
using Northwoods.Go.Models;
using Northwoods.Go.PanelLayouts;
using RowDefinition = Northwoods.Go.RowDefinition;
using Stretch = Northwoods.Go.Stretch;
using TextBlock = Northwoods.Go.TextBlock;

namespace DWK.Diagram;

public class LogicDiagramBuilder
{
    private LogicDiagramSettings Settings { get; } = new();

    private JsonSerializerOptions JsonSerializerOptions { get; } = new(JsonSerializerOptions.Web)
    {
        WriteIndented = true
    };

    public LogicDiagramBuilder WithSettings(Action<LogicDiagramSettings> action)
    {
        action.Invoke(Settings);
        return this;
    }

    public void BuildDiagram(Northwoods.Go.Diagram diagram)
    {
        string[] import = ["in-a", "in-a", "in-b"];
        string[] export = ["res"];

        diagram.ToolManager.HoverDelay = 750;


        diagram.NodeTemplate = MakeNodeTemplate(import.ToHashSet(), export.ToHashSet());
        diagram.LinkTemplate = MakeLinkTemplate();

        diagram.Model = new LogicModel
        {
            NodeDataSource = new List<LogicNodeData>
            {
                new() { Key = Guid.NewGuid(), Tag = "Alpha", Color = "lightblue" },
                new() { Key = Guid.NewGuid(), Tag = "Beta", Color = "orange" },
            },
            LinkDataSource = new List<LogicLinkData>
            {
            }
        };
    }

    private Node MakeNodeTemplate(ISet<string> imports, ISet<string> exports)
    {
        var portRows = int.Max(imports.Count, exports.Count);
        var iPos = (portRows - imports.Count) / 2;
        var oPos = (portRows - exports.Count) / 2;

        var table = new Node(PanelLayoutTable.Instance)
            .Add(new RowDefinition { Row = portRows + 1, Height = 20 });

        // border rect
        table.Add(new Shape("rectangle")
        {
            Name = "PART_Border",
            Row = 0, RowSpan = portRows + 2,
            Column = 1, ColumnSpan = 2,
            Fill = Settings.ModuleBackColor,
            Stroke = Settings.ModuleTextColor,
            StrokeWidth = 1.5,
            Stretch = Stretch.Fill
        });

        // header text
        table.Add(new TextBlock("Header")
        {
            Row = 0, Column = 1, ColumnSpan = 2,
            Stretch = Stretch.Horizontal,
            TextAlign = TextAlign.Center,
            Font = Settings.HeadFont,
            Stroke = Settings.ModuleTextColor,
            Margin = new Margin(4, 0)
        });

        // add imports
        foreach (var port in imports)
        {
            iPos += 1;
            table.Add(new TextBlock(port)
            {
                Row = iPos, Column = 1,
                Font = Settings.PortFont, 
                Stroke = Settings.ModuleTextColor,
                Margin = new Margin(0, 12, 0, 4),
                Alignment = Spot.Left,
            });

            table.Add(new Shape("rectangle")
            {
                Row = iPos, Column = 0,
                Height = 6, Width = 6,
                Stroke = Settings.ModuleTextColor,
                Fill = Settings.ModuleTextColor,
                Alignment = Spot.Right,
                Cursor = "pointer",

                PortId = port,
                ToLinkable = true,
                ToLinkableSelfNode = true
            });
        }

        // add exports
        foreach (var port in exports)
        {
            oPos += 1;
            table.Add(new TextBlock(port)
            {
                Row = oPos, Column = 2,
                Font = Settings.PortFont,
                Stroke = Settings.ModuleTextColor,
                Margin = new Margin(0, 4, 0, 12),
                Alignment = Spot.Right,
            });

            table.Add(new Shape("rectangle")
            {
                Row = oPos, Column = 3,
                Height = 6, Width = 6,
                Stroke = Settings.ModuleTextColor,
                Fill = Settings.ModuleTextColor,
                Alignment = Spot.Left,
                Cursor = "pointer",

                PortId = port,
                FromLinkable = true,
                FromLinkableSelfNode = true
            });
        }

        // tag text
        table.Add(new TextBlock
            {
                Row = portRows + 2,
                Column = 0, ColumnSpan = 4,
                Font = Settings.TagFont,
                Stroke = Settings.ModuleTextColor,
                TextAlign = TextAlign.Center,
                Stretch = Stretch.Horizontal
            }
            .Bind(nameof(TextBlock.Text), nameof(LogicNodeData.Tag))
        );

        // set to port max link count = 1
        table.LinkValidation = (fromNode, fromPort, toNode, toPort, link) => !toNode.FindLinksConnected(toPort.PortId).Any();

        // set tooltip
        table.ToolTip = new Adornment(PanelLayoutAuto.Instance)
            .Add(new Shape("Rectangle") { Fill = Settings.NodeTipBackColor, Stroke = Settings.NodeTipForeColor })
            .Add(new TextBlock { Font = Settings.CodeFont, Margin = 12, Stroke = Settings.NodeTipForeColor }
                .Bind(nameof(TextBlock.Text), string.Empty, value =>
                {
                    if (value is not LogicNodeData node) return "Node Type Error";
                    var json = JsonSerializer.Serialize(node, JsonSerializerOptions);
                    return $"{nameof(LogicNodeData)}:\n{json}";
                }));

        return table;
    }

    private Link MakeLinkTemplate()
    {
        var link = new Link
        {
            Corner = 6,
            ToShortLength = 3,
            Reshapable = true, 
            Resegmentable = true,
            Routing = LinkRouting.Orthogonal,// LinkRouting.AvoidsNodes,
            FromSpot = Spot.Right, ToSpot = Spot.Left,
            RelinkableFrom = false, RelinkableTo = true,
            MouseEnter = (e, l, _) => { ((l as Link).Elt(2) as Shape).Stroke = "rgba(0,90,156,.3)"; },
            MouseLeave = (e, l, _) => { ((l as Link).Elt(2) as Shape).Stroke = "transparent"; }
        }
        .Bind(new Binding("Points").MakeTwoWay());

        var tooltip = new Adornment(PanelLayoutAuto.Instance)
            .Add(new Shape("Rectangle") { Fill = Settings.LinkTipBackColor, Stroke = Settings.LinkTipForeColor })
            .Add(new TextBlock { Margin = 12, Font = Settings.CodeFont, Stroke = Settings.LinkTipForeColor }
                .Bind(nameof(TextBlock.Text), "", value =>
                {
                    if (value is not LogicLinkData link) return "Link Type Error";
                    var json = JsonSerializer.Serialize(link, JsonSerializerOptions);
                    return $"{nameof(LogicLinkData)}:\n{json}";
                }));

        link.Add(
            new Shape { ToArrow = "Standard", Stroke = null, Fill = Settings.LinkColor },
            new Shape { IsPanelMain = true, Stroke = Settings.LinkColor, StrokeWidth = 2 },
            new Shape { IsPanelMain = true, Stroke = "transparent", StrokeWidth = 6, ToolTip = tooltip }
        );
        
        return link;
    }
}