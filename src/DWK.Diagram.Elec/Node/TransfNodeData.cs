namespace DWK.Diagram.Node;

public class TransfNodeData : ElecNodeData
{
    public TransfNodeData()
    {
        Tag = Category = ElecNodeCategory.Transformer;
    }

    public override string Category
    {
        get => base.Category;
        set => base.Category = ElecNodeCategory.Transformer;
    }
}