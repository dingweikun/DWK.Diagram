using DWK.Diagram.ElecModels;

namespace DWK.Diagram.Node;

public class LoadNodeData : ElecNodeData, IResizedWidth
{
    public LoadNodeData()
    {
        base.Category = Tag = ElecNodeCategory.Load;
    }

    public double ResizedWidth { get; set; } = LoadNodeTemplate.DefaultWidth;
}