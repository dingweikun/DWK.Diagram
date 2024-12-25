using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DWK.Diagram.Logic;
using Northwoods.Go.Models;

namespace DWK.Diagram.Views;

public partial class DiagramView : UserControl
{
    public DiagramView()
    {
        InitializeComponent();

        // var fontName = this.FontFamily.Name;
        //
        // PART_DiagramControl.Diagram.Add(
        //     new Part("Vertical")
        //         .Add(
        //             new GoTextBlock { Text = "a Text Block" },
        //             new GoTextBlock { Text = "a Text Block", Stroke = "red" },
        //             new GoTextBlock { Text = "a Text Block", Background = "lightblue" },
        //             new GoTextBlock { Text = "a Text Block", Font = new Font(fontName, 13, Northwoods.Go.FontWeight.Bold) }
        //         )
        // );

        // Setup(PART_DiagramControl.Diagram);
        Setup();
        
    }

    private static void Setup(Go.Diagram myDiagram)
    {
        // diagram properties
        myDiagram.UndoManager.IsEnabled = true; // enable undo & redo

        // myDiagram.NodeTemplate = LogicDiagram.MakeNodeTemplate();

        // string[] import = ["in-a", "in-b", "in-a", "in-b", "in-a", "in-b"];
        // string[] export = ["res-a", "res-b", "results-test", "in-a", "in-b"];
        string[] import = ["in-a", "in-a", "in-b"];
        string[] export = ["res"];
        myDiagram.NodeTemplate = LogicDiagram.MakeNodeTemplate(import, export);
        myDiagram.LinkTemplate = LogicDiagram.MakeLinkTemplate();
 

        // myDiagram.Model = new LogicModel()
        // {
        //     // for each object in this list, the Diagram creates a Node to represent it
        //     NodeDataSource = new List<LogicNodeData>
        //     {
        //         new LogicNodeData { Key = Guid.NewGuid(), Text = "Alpha", Color = "lightblue" },
        //         new LogicNodeData { Key = Guid.NewGuid(), Text = "Beta", Color = "orange" },
        //         new LogicNodeData { Key = Guid.NewGuid(), Text = "Gamma", Color = "lightgreen" },
        //         new LogicNodeData { Key = Guid.NewGuid(), Text = "Delta", Color = "pink" }
        //     }
        // };
    }
    
    
    private void Setup() {
      var _Diagram = PART_DiagramControl.Diagram;

      // diagram properties

      // allow double-click in background to create a new node
      _Diagram.ToolManager.ClickCreatingTool.ArchetypeNodeData = new NodeData { Text = "Node", Color = "gray" };
      // allow Ctrl-G to call GroupSelection()
      _Diagram.CommandHandler.ArchetypeGroupData = new NodeData { Text = "Group", IsGroup = true, Color = "blue" };
      // enable undo and redo
      _Diagram.UndoManager.IsEnabled = true;

      // Define the appearance and behavior for Nodes:

      // First, define the shared context menu for all Nodes, Links, and Groups.

      // To simplify this code we define a function for creating a context menu button:

      // a context menu is an Adornment with a bunch of buttons in them
     

      string NodeInfo(object d, object o = null) {  // Tooltip info for a node data object
        var nd = d as NodeData;
        var str = "Node " + nd.Key + ": " + nd.Text + "\n";
        if (nd.Group != default) {
          str += "member of " + nd.Group;
        } else {
          str += "top-level node";
        }
        return str;
      }

      // These nodes have text surrounded by a rounded rectangle
      // whose fill color is bound to the node data.
      // The user can drag a node by dragging its TextBlock label.
      // Dragging from the Shape will start drawing a new link.
      _Diagram.NodeTemplate =
        new Go.Node(Go.PanelType.Auto) {
          LocationSpot = Go.Spot.Center,
          // this tooltip Adornment is shared by all nodes
      
        }
          .Add(
            new Go.Shape("RoundedRectangle") {
                Fill = "white", // the default fill, if there is no data bound value
                PortId = "", Cursor = "pointer",  // the Shape is the port, not the whole Node
                // allow all kinds of links from and to this port
                FromLinkable = true, FromLinkableSelfNode = true, FromLinkableDuplicates = true,
                ToLinkable = true, ToLinkableSelfNode = true, ToLinkableDuplicates = true
              }
              .Bind("Fill", "Color"),
            new Go.TextBlock {
                Font = new Go.Font("Segoe UI", 14, Northwoods.Go.FontWeight.Bold),
                Stroke = "#333",
                Margin = 6,  // make some extra space for the shape around the text
                IsMultiline = false,  // don't allow newlines in text
                Editable = true  // allow in-place editing by user
              }
              .Bind(new Binding("Text").MakeTwoWay())  // the label shows the node data's text
          );

      // Define the appearance and behavior for Links:

      string LinkInfo(object d, object o = null) {  // Tooltip info for a link data object
        var ld = d as LinkData;
        return "Link " + ld.Key + ":\nfrom " + ld.From + " to " + ld.To;
      }

      // The link shape and arrowhead have their stroke brush data bound to the "color" property
      _Diagram.LinkTemplate =
        new Go.Link {
            ToShortLength = 3,
            // allow the user to relink existing links
            RelinkableFrom = true,
            RelinkableTo = true,
            
          }
          .Add(  // allow the user to relink existing links
            new Go.Shape { StrokeWidth = 2 }.Bind("Stroke", "Color"),
            new Go.Shape { ToArrow = "Standard", Stroke = null }.Bind("Fill", "Color")
          );

      // Define the appearance and behavior for Groups:

      
      // Define the behavior for the Diagram background:

      string DiagramInfo(object m, object o = null) {  // Tooltip info for the diagram's model
        var model = m as Model;
        return "Model:\n" + model.NodeDataSource.Count() + " nodes, " + model.LinkDataSource.Count() + " links";
      }

      // provide a tooltip for the background of the Diagram, when not over any Part
    

      // Create the Diagram's Model:
      _Diagram.Model = new Model {
        NodeDataSource = new List<NodeData> {
          new NodeData { Key = 1, Text = "Alpha", Color = "lightblue" },
          new NodeData { Key = 2, Text = "Beta", Color = "orange" },
          new NodeData { Key = 3, Text = "Gamma", Color = "lightgreen", Group = 5 },
          new NodeData { Key = 4, Text = "Delta", Color = "pink", Group = 5 },
          new NodeData { Key = 5, Text = "Epsilon", Color = "green", IsGroup = true }
        },
        LinkDataSource = new List<LinkData> {
          new LinkData { From = 1, To = 2, Color = "blue" },
          new LinkData { From = 2, To = 2 },
          new LinkData { From = 3, To = 4, Color = "green" },
          new LinkData { From = 3, To = 1, Color = "purple" }
        }
      };
    }
    
}


// define the model data
public class Model : GraphLinksModel<NodeData, int, object, LinkData, string, string> { }
public class NodeData : Model.NodeData {
    public string Color { get; set; }
}

public class LinkData : Model.LinkData {
    public string Color { get; set; }
}