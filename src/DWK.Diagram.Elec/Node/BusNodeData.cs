namespace DWK.Diagram.Node;

public class BusNodeData : ElecNodeData
{
    public BusNodeData()
    {
        base.Category = Tag = ElecNodeCategory.Bus;
    }

    public double Width { get; set; } = 200;
}