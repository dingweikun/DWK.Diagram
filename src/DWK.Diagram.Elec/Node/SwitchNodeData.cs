namespace DWK.Diagram.Node;

public class SwitchNodeData : ElecNodeData, IOrientation, IToggle
{
    public SwitchNodeData()
    {
        base.Category = Tag = ElecNodeCategory.Switch;
    }

    public bool Opened { get; set; } = true;

    public bool IsVertical { get; set; } = true;
}