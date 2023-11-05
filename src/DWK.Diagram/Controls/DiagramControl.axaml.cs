using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Northwoods.Go.Models;

namespace DWK.Diagram.Controls;

public partial class DiagramControl : UserControl
{
    private Northwoods.Go.Diagram myDiagram;

    public DiagramControl()
    {
        InitializeComponent();

        myDiagram = PartDiagram.Diagram;
        Setup();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }


    private void Setup()
    {
        // diagram properties
        myDiagram.UndoManager.IsEnabled = true; // enable undo & redo

        myDiagram.Model = new MyModel
        {
            // for each object in this list, the Diagram creates a Node to represent it
            NodeDataSource = new List<NodeData>
            {
                new NodeData { Key = "Alpha" },
                new NodeData { Key = "Beta" },
                new NodeData { Key = "Gamma" }
            }
        };
    }
}



// define the model data
public class MyModel : Model<NodeData, string, object>
{
}

public class NodeData : MyModel.NodeData
{
}