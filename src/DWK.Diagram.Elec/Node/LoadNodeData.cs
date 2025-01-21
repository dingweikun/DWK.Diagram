using DWK.Diagram.ElecModels;

namespace DWK.Diagram.Node;

public class LoadNodeData : ElecNodeData
{
    public LoadNodeData()
    {
        Tag = Category = ElecNodeCategory.Load;
    }

    public override string Category
    {
        get => base.Category;
        set => base.Category = ElecNodeCategory.Load;
    }
}