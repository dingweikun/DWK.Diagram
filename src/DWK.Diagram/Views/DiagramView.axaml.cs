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

        Setup(PART_DiagramControl.Diagram);
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
        myDiagram.NodeTemplate = LogicDiagram.MakeTable(import, export);

        // myDiagram.NodeTemplate =
        //     new Go.Node(Go.PanelLayouts.PanelLayoutAuto.Instance)
        //         {
        //             Width = 100, Height = 60, Background = "orange"
        //         }
        //         .Add(
        //             new Go.Shape("Rectangle")
        //                 {
        //                     Stroke = null
        //                 }
        //                 // the Shape.Fill comes from the Node.Data.Color property
        //                 .Bind(nameof(Go.Shape.Fill), nameof(LogicNodeData.Color)),
        //             new Go.TextBlock
        //                 {
        //                     // leave some space around larger-than-normal text
        //                     Margin = 6, Font = new Go.Font(_fontName, 18)
        //                 }
        //                 // the TextBlock.Text comes from the Node.Data.Text property
        //                 .Bind(nameof(Go.TextBlock.Text), nameof(LogicNodeData.Text))
        //         );


        myDiagram.Model = new LogicModel()
        {
            // for each object in this list, the Diagram creates a Node to represent it
            NodeDataSource = new List<LogicNodeData>
            {
                new LogicNodeData { Key = Guid.NewGuid(), Text = "Alpha", Color = "lightblue" },
                new LogicNodeData { Key = Guid.NewGuid(), Text = "Beta", Color = "orange" },
                new LogicNodeData { Key = Guid.NewGuid(), Text = "Gamma", Color = "lightgreen" },
                new LogicNodeData { Key = Guid.NewGuid(), Text = "Delta", Color = "pink" }
            }
        };
    }
}