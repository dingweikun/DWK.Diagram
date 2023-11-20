using Avalonia.Controls;
using Northwoods.Go;
using Northwoods.Go.Models;
using Northwoods.Go.PanelLayouts;
using System;
using System.Collections.Generic;

namespace DWK.Diagram.Views;

public partial class ModuleLibraryPanel : UserControl
{
    public ModuleLibraryPanel()
    {
        InitializeComponent();

        Palette palette = PartPalette.Diagram as Palette ?? throw new System.Exception();

        palette.NodeTemplate = new Node(PanelLayoutSpot.Instance)
            .Add(new Shape("Rectangle") { Width = 40, Height = 40 }.Bind("Fill", "Color"))
            .Add(new Northwoods.Go.TextBlock("???") { Font = new Font("Segoe UI", 10) }.Bind("Text", "Category"));

        palette.Model = new GGModel()
        {
            NodeDataSource = new List<NodeData>
            {
                new NodeData { Category = "C", Color = "cyan" },
                new NodeData { Category = "LC", Color = "lightcyan" },
                new NodeData { Category = "A", Color = "aquamarine" },
                new NodeData { Category = "T", Color = "turquoise" },
                new NodeData { Category = "PB", Color = "powderblue" },
                new NodeData { Category = "LB", Color = "lightblue" },
                new NodeData { Category = "LSB", Color = "lightskyblue" },
                new NodeData { Category = "DSB", Color = "deepskyblue" }
            }
        };


    }
}

public class GGModel : Model<NodeData, string, object>
{
}

public class NodeData : GGModel.NodeData
{
    public string Color { get; set; } = string.Empty;
}

public class DataPack<T> where T : ValueType
{ }