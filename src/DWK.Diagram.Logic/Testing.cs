using Northwoods.Go;
using Northwoods.Go.Layouts;
using Northwoods.Go.Models;
using Northwoods.Go.PanelLayouts;

namespace DWK.Diagram;

// define the model data

public static class Testing
{
    public static void Setup0(Northwoods.Go.Diagram diagram)
    {
        var style = new GridStyle();
        diagram.Grid =
            new Panel(PanelLayoutGrid.Instance)
                {
                    GridCellSize = new Size(10, 10),
                    Background = style.BackBrush,
                }
                .Add(
                    new Shape("LineH").Bind(nameof(Shape.Stroke), "", value =>
                    {
                        var type = value.GetType();
                        Console.WriteLine(type.FullName);
                        return "yellow";
                    }),
                    new Shape("LineV") { Stroke = style.SubLineBrush },
                    new Shape("LineH") { Stroke = style.MainLineBrush, Interval = 5 },
                    new Shape("LineV") { Stroke =  style.MainLineBrush, Interval = 5 }
                );
        diagram.Grid.Visible = true;
        
    }

    public static void Setup3(Northwoods.Go.Diagram diagram)
    {
        var set = new LogicDiagramSettings();

        diagram.NodeTemplate =
            new Node("Auto")
                {
                    LinkValidation = (fromnode, fromport, tonode, toport, link) =>
                    {
                        // total number of links connecting with a port is limited to 1:
                        return tonode.FindLinksConnected(toport.PortId).Count() < 1;
                    }
                }
                .Bind("Location", "Loc", Point.Parse)
                .Add(new Shape("Rectangle")
                {
                    // PortId = "",  FromLinkable = false, ToLinkable = false,

                    Fill = "LightYellow",
                })
                .Add(new Panel(PanelLayoutVertical.Instance)
                    .Add(
                        new TextBlock { Stroke = "Black", Margin = 3, Font = set.HeadFont }
                            .Bind("Text", "Key"),
                        new Shape("Ellipse")
                        {
                            DesiredSize = new Size(10, 10),
                            Fill = "green", PortId = "In", Cursor = "pointer",

                            FromLinkable = false,
                            ToLinkable = true,


                            ToMaxLinks = 1,
                            FromLinkableDuplicates = false,
                            ToLinkableDuplicates = false,

                            FromLinkableSelfNode = true,
                            ToLinkableSelfNode = true
                        },
                        // .Bind("FromLinkable", "From")
                        // .Bind("ToLinkable", "To"),
                        new TextBlock("In(To)") { Stroke = "Black", Margin = 3, Font = set.PortFont },
                        new Shape("Ellipse")
                        {
                            DesiredSize = new Size(10, 10),
                            Fill = "red", PortId = "Out", Cursor = "pointer",

                            FromLinkable = true,
                            ToLinkable = false,

                            // ToMaxLinks = 1,
                            FromLinkableDuplicates = false,
                            ToLinkableDuplicates = false,

                            FromLinkableSelfNode = true,
                            ToLinkableSelfNode = true
                        },
                        // .Bind("FromLinkable", "From")
                        // .Bind("ToLinkable", "To"),
                        new TextBlock("Out(From)") { Stroke = "Black", Margin = 3, Font = set.PortFont }
                    )
                );

        diagram.Model =
            new MyModel
            {
                NodeDataSource = new List<NodeData>
                {
                    new NodeData { Key = "Any", Loc = "0 0", From = true, To = true },
                    new NodeData { Key = "From1", Loc = "0 0", From = true },
                    new NodeData { Key = "From2", Loc = "0 100", From = true },
                    new NodeData { Key = "To1", Loc = "150 0", To = true },
                    new NodeData { Key = "To2", Loc = "150 100", To = true }
                },
                LinkDataSource = new List<LinkData>
                {
                    // initially no links
                }
            };
    }

    public static void Setup2(Northwoods.Go.Diagram diagram)
    {
        var set = new LogicDiagramSettings();

        diagram.NodeTemplate = new Node("Auto")
            .Add(
                new Shape { Fill = "lightgray" },
                new Panel("Table")
                    .Add(
                        new ColumnDefinition { Column = 0, Alignment = Spot.Left },
                        new ColumnDefinition { Column = 2, Alignment = Spot.Right }
                    )
                    .Add(
                        new TextBlock
                            {
                                // the node title
                                Column = 0, Row = 0, ColumnSpan = 3, Alignment = Spot.Center,
                                Font = set.HeadFont, Margin = new Margin(4, 2)
                            }
                            .Bind("Text", "Key"),
                        new Panel("Horizontal") { Column = 0, Row = 1 }
                            .Add(
                                // the "A" port
                                new Shape
                                {
                                    Width = 6, Height = 6, PortId = "A", ToSpot = Spot.Left,
                                    ToLinkable = true, ToMaxLinks = 1 // allow user-drawn links to here
                                },
                                new TextBlock("A") { Font = set.PortFont }
                            ),
                        new Panel("Horizontal") { Column = 0, Row = 2 }
                            .Add(
                                // the "B" port
                                new Shape { Width = 6, Height = 6, PortId = "B", ToSpot = Spot.Left },
                                new TextBlock("B") { Font = set.PortFont }
                            ),
                        new Panel("Horizontal") { Column = 2, Row = 1, RowSpan = 2 }
                            .Add(
                                // the "Out" port
                                new TextBlock("Out") { Font = set.PortFont },
                                new Shape
                                {
                                    Width = 6, Height = 6, PortId = "Out", FromSpot = Spot.Right,
                                    FromLinkable = true // allow user-drawn links from here
                                }
                            )
                    )
            );

        diagram.LinkTemplate =
            new Link { Routing = LinkRouting.Orthogonal, Corner = 3 }
                .Add(
                    new Shape(),
                    new Shape { ToArrow = "Standard" }
                );

        diagram.Layout = new LayeredDigraphLayout { ColumnSpacing = 10 };

        diagram.Model =
            new MyModel
            {
                LinkFromPortIdProperty = "FromPort", // required information:
                LinkToPortIdProperty = "ToPort", // identifies data property names
                NodeDataSource = new List<NodeData>
                {
                    new NodeData { Key = "Add1" },
                    new NodeData { Key = "Add2" },
                    new NodeData { Key = "Subtract1" }
                },
                LinkDataSource = new List<LinkData>
                {
                    // new LinkData { From = "Add1", FromPort = "Out", To = "Subtract1", ToPort = "A" },
                    // new LinkData { From = "Add2", FromPort = "Out", To = "Subtract1", ToPort = "B" }
                }
            };
    }
    
    public static void Setup(Northwoods.Go.Diagram diagram)
    {
        var set = new LogicDiagramSettings();

        // enable Ctrl-Z to undo and Ctrl-Y to redo
        diagram.UndoManager.IsEnabled = true;

        // the node template describes how each Node should be constructed
        diagram.NodeTemplate =
            new Node("Auto")
                .Bind("Location", "Loc", Point.Parse) // convert string into a Point value
                .Add(
                    new Shape("RoundedRectangle")
                        {
                            PortId = "",
                            Fill = "white"
                        }
                        .Bind("Fill", "Color"),
                    new TextBlock
                        {
                            Margin = 5,
                            Font = set.TagFont
                        }
                        .Bind("Text", "Key")
                );

        var nodeDataList = new List<NodeData>
        {
            new() { Key = "Alpha", Color = "lightblue", Loc = "0 0" }, // note string values for location
            new() { Key = "Beta", Color = "pink", Loc = "100 50" }
        };

        var linkDataList = new List<LinkData>
        {
            new() { From = "Alpha", To = "Beta" }
        };

        diagram.Model = new MyModel
        {
            NodeDataSource = nodeDataList,
            LinkDataSource = linkDataList
        };
    }
}

// define the model data
public class MyModel : GraphLinksModel<NodeData, string, object, LinkData, string, string>
{
    public MyModel()
    {
        LinkFromKeyProperty = nameof(LogicLinkData.From);
        LinkFromPortIdProperty = nameof(LogicLinkData.FromPort);

        LinkToKeyProperty = nameof(LogicLinkData.To);
        LinkToPortIdProperty = nameof(LogicLinkData.ToPort);
    }
}

public class NodeData : MyModel.NodeData
{
    public string Color { get; set; }
    public string Loc { get; set; }
    public bool From { get; set; }
    public bool To { get; set; }
}

public class LinkData : MyModel.LinkData
{
}