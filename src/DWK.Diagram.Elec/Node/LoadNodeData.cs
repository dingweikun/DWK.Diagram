using DWK.Diagram.ElecModels;

namespace DWK.Diagram.Node;

public class LoadNodeData : ElecNodeData
{
    public LoadNodeData()
    {
        base.Category = Tag = ElecNodeCategory.Load;
    }
}