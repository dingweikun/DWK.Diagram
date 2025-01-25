namespace DWK.Diagram.Node;

public class MotorNodeData : ElecNodeData, IResizedWidth
{
    public MotorNodeData()
    {
        base.Category = Tag = ElecNodeCategory.Motor;
    }

    public double ResizedWidth { get; set; } = MotorNodeTemplate.DefaultWidth;
}