namespace DWK.Diagram.Node;

public class BusPointNodeData : ElecNodeData
{
    public BusPointNodeData()
    {
        base.Category = Tag = ElecNodeCategory.BusPoint;
    }
}