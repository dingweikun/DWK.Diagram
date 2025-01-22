namespace DWK.Diagram.Node;

public class TransfNodeData : ElecNodeData
{
    public TransfNodeData()
    {
        base.Category = Tag = ElecNodeCategory.Transformer;
    }
}