namespace DWK.Diagram.Node;

public class ElecDiagramTheme
{
    public GoFont DefaultFont { get; set; } = new("Noto Sans CJK SC", 16);

    public GoFont TagFont { get; set; } = new("Noto Sans CJK SC", 18);

    public GoFont TinyFont { get; set; } = new("Noto Sans CJK SC", 10);
    
    public GoBrush DefaultTextBrush { get; set; } = "black";
    public GoBrush DefaultTextBackBrush { get; set; } = "transparent";

    public GoBrush AdornmentStrokeBrush { get; set; } = "dodgerblue";

    public GoBrush AdornmentFillBrush { get; set; } = "lightblue";

    public GoBrush PortBrush { get; set; } = "black";

    // Link Style
    public GoBrush LinkBrush { get; set; } = "black";

    // Bus Node Style
    public GoBrush BusLineBrush { get; set; } = "red";
    public GoBrush BusBodyBrush { get; set; } = "lightyellow";

    // Wire Node Style
    public GoBrush WireBrush { get; set; } = "green";
}