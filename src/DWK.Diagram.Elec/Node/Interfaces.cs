namespace DWK.Diagram.Node;

public interface IOrientation
{
    bool IsVertical { get; set; }
}

public interface IResizedWidth
{
    double ResizedWidth { get; set; }
}

public interface IToggle
{
    bool Opened { get; set; }
}