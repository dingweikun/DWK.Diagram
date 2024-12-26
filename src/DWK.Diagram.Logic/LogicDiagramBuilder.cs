using Northwoods.Go;
using Northwoods.Go.Models;
using Northwoods.Go.PanelLayouts;
using FontWeight = Northwoods.Go.FontWeight;
using Stretch = Northwoods.Go.Stretch;

namespace DWK.Diagram;

public class LogicDiagramBuilder
{
    private LogicDiagramSettings Settings { get; } = new();

    public LogicDiagramBuilder WithSettings(Action<LogicDiagramSettings> action)
    {
        action.Invoke(Settings);
        return this;
    }

    public void BuildDiagram(Northwoods.Go.Diagram diagram)
    {
        string[] import = ["in-a", "in-a", "in-b"];
        string[] export = ["res"];

        diagram.NodeTemplate = MakeNodeTemplate(import.ToHashSet(), export.ToHashSet());
        diagram.LinkTemplate = MakeLinkTemplate();
        
        
        diagram.Model = new LogicModel
        {
            NodeDataSource = new List<LogicNodeData>
            {
                new LogicNodeData { Key = 1, Tag = "Alpha", Color = "lightblue" },
                new LogicNodeData { Key = 2, Tag = "Beta", Color = "orange" },
                // new LogicNodeData { Key = 3, Tag = "Gamma", Color = "lightgreen", Group = 5 },
                // new LogicNodeData { Key = 4,  Color = "pink", Group = 5 },
                // new LogicNodeData { Key = 5, Tag = "Epsilon", Color = "green", IsGroup = true }
            },
            // LinkDataSource = new List<LogicLinkData>
            // {
            //     new LogicLinkData { From = 1, To = 2, Color = "blue" },
            //     new LogicLinkData { From = 2, To = 2 },
            //     new LogicLinkData { From = 3, To = 4, Color = "green" },
            //     new LogicLinkData { From = 3, To = 1, Color = "purple" }
            // }
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
                Margin = new Margin(0, 12, 0, 4),
                Alignment = Spot.Left,
            });

            table.Add(new Shape("rectangle")
            {
                Row = iPos, Column = 0,
                Height = 6, Width = 6,
                Alignment = Spot.Right,
                Cursor = "pointer",

                PortId = port,
                ToLinkable = true,
                ToLinkableSelfNode = true,
                ToLinkableDuplicates = false,
                ToSpot = Spot.Left,
                ToMaxLinks = 1
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
                Margin = new Margin(0, 4, 0, 12),
                Alignment = Spot.Right,
                Cursor = "pointer"
            });

            table.Add(new Shape("rectangle")
            {
                Row = oPos, Column = 3,
                Height = 6, Width = 6,
                Alignment = Spot.Left,
                Cursor = "pointer",

                PortId = port,
                FromLinkable = true,
                FromLinkableSelfNode = true,
                FromLinkableDuplicates = false,
                FromSpot = Spot.Right,
            });
        }

        // tag text
        table.Add(new TextBlock
            {
                Row = portRows + 2,
                Column = 0, ColumnSpan = 4,
                Font = Settings.TagFont,
                TextAlign = TextAlign.Center,
                Stretch = Stretch.Horizontal
            }
            .Bind(nameof(TextBlock.Text), nameof(LogicNodeData.Tag))
        );

        return table;
    }

    private Link MakeLinkTemplate()
    {
        var link = new Link
        {
            ToShortLength = 3,
            RelinkableFrom = true,
            RelinkableTo = true,

            Routing = LinkRouting.AvoidsNodes,
            Corner = 6
        };

        link.Add(
            new Shape { Stroke = Settings.LinkColor, StrokeWidth = 2 },
            new Shape { ToArrow = "Standard", Stroke = null, Fill = Settings.LinkColor }
        );

        return link;
    }
}