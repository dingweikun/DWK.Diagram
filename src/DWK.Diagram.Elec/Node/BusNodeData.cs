namespace DWK.Diagram.Node;

public class BusNodeData : ElecNodeData, IResizedWidth
{
    public BusNodeData()
    {
        base.Category = Tag = ElecNodeCategory.Bus;
    }

    public double ResizedWidth { get; set; } = BusNodeTemplate.DefaultWidth;
}