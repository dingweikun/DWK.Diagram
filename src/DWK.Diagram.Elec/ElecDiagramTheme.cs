namespace DWK.Diagram.Node;

public class ElecDiagramTheme
{
    public GoFont DefaultFont { get; set; } = new("Noto Sans CJK SC", 20);

    public GoBrush AdornmentStrokeBrush { get; set; } = "dodgerblue";

    public GoBrush AdornmentFillBrush { get; set; } = "lightblue";


    // Bus Node Style
    public double BusStrokeWidth { get; set; } = 2;
    public GoBrush BusStrokeBrush { get; set; } = "red";
}