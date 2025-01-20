using DWK.Diagram.ElecModels;

namespace DWK.Diagram.Node;

public class LoadNodeData : ElecNodeData
{
    public LoadNodeData()
    {
        Ports = new Dictionary<string, EPort>
        {
            {
                "EP",
                new EPort(EPortType.NODE, EPortType.NODE | EPortType.NODE)
            }
        };

        Tag = Category = ElecNodeCategory.Load;
    }

    public override string Category
    {
        get => base.Category;
        set => base.Category = ElecNodeCategory.Load;
    }
}