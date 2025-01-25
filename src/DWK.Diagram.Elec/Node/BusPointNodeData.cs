namespace DWK.Diagram.Node;

public class BusPointNodeData : ElecNodeData, IResizedWidth
{
    public BusPointNodeData()
    {
        base.Category = Tag = ElecNodeCategory.BusPoint;
    }

    public double ResizedWidth { get; set; } = BusPointNodeTemplate.DefaultWidth;
}