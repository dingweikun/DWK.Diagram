namespace DWK.Diagram.Node;

public class BreakerNodeData : ElecNodeData, IOrientation
{
    public BreakerNodeData()
    {
        base.Category = Tag = ElecNodeCategory.Breaker;
    }

    public bool IsVertical { get; set; }
}