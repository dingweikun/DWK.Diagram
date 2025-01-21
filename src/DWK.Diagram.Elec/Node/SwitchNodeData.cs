namespace DWK.Diagram.Node;

public class SwitchNodeData : ElecNodeData, IOrientation
{
    public SwitchNodeData()
    {
        Tag = Category = ElecNodeCategory.Switch;
    }

    public override string Category
    {
        get => base.Category;
        set => base.Category = ElecNodeCategory.Switch;
    }

    public bool Opened { get; set; } = true;

    public bool IsVertical { get; set; }
}