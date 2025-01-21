using DWK.Diagram.ElecModels;
using Northwoods.Go.Models;

namespace DWK.Diagram.Node;

public class LineNodeData : ElecNodeData, IOrientation
{
    public LineNodeData()
    {
        Tag = Category = ElecNodeCategory.Line;
    }

    public override string Category
    {
        get => base.Category;
        set => base.Category = ElecNodeCategory.Line;
    }

    public bool IsVertical { get; set; }
}