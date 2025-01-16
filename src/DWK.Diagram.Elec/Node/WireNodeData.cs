namespace DWK.Diagram.Node;

public interface IAngle
{
    double Angle { get; set; }
}

public interface IOrientation
{
    bool IsVertical { get; set; }
}

public class WireNodeData : ElecNodeData, IAngle
{
    public WireNodeData()
    {
        Category = ElecNodeCategory.Wire;
    }

    public override string Category
    {
        get => base.Category;
        set => base.Category = ElecNodeCategory.Wire;
    }

    public double Width { get; set; } = 200;

    public double Angle { get; set; } = 0;

    public double ImpedanceInOhms { get; set; } = 10;
}