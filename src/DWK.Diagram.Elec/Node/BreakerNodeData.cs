namespace DWK.Diagram.Node;

public class BreakerNodeData : ElecNodeData, IOrientation, IToggle
{
    public BreakerNodeData()
    {
        base.Category = Tag = ElecNodeCategory.Breaker;
    }

    public bool Opened { get; set; }

    public bool IsVertical { get; set; } = true;
}