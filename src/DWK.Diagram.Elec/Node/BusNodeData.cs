namespace DWK.Diagram.Node;

public class BusNodeData : ElecNodeData
{
    public BusNodeData()
    {
        Category = ElecNodeCategory.Bus;
    }

    public override string Category
    {
        get => base.Category;
        set => base.Category = ElecNodeCategory.Bus;
    }

    public double Width { get; set; } = 200;
}