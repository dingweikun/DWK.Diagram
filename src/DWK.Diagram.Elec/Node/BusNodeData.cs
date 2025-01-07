namespace DWK.Diagram.Node;

public class BusNodeData : ElecNodeData
{
    internal const double InitLength = 200;

    public override string Category
    {
        get => base.Category;
        set => base.Category = ElecNodeCategory.Bus;
    }

    public double Length { get; set; } = InitLength;
}