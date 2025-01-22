namespace DWK.Diagram.Node;

public class LineNodeData : ElecNodeData, IOrientation
{
    public LineNodeData()
    {
        base.Category = Tag = ElecNodeCategory.Line;
    }


    public bool IsVertical { get; set; }
}