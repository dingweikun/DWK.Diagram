using Northwoods.Go;

namespace DWK.Diagram;

public class LogicDiagramSettings : BaseDiagramSettings
{
    public Size PortSize { get; set; }

    public Font TagFont { get; set; }

    public Font HeadFont { get; set; }

    public Font PortFont { get; set; }

    public Font TipFont { get; set; }

    public Font CodeFont { get; set; }

    public string ModuleBackColor { get; set; }

    public string ModuleTextColor { get; set; }

    public string NodeTipBackColor { get; set; }

    public string NodeTipForeColor { get; set; }

    public string LinkTipBackColor { get; set; }

    public string LinkTipForeColor { get; set; }

    public LogicDiagramSettings()
    {
        PortSize = new Size(6, 6);

        TagFont = new Font(FontName, 15, FontWeight.Regular);
        TipFont = new Font(FontName, 15, FontWeight.Regular);
        HeadFont = new Font(FontName, 15, FontWeight.Bold);
        PortFont = new Font(FontName, 13, FontWeight.Regular);
        CodeFont = new Font(CodeFontName, 15, FontWeight.Regular);

        ModuleBackColor = "LightBlue";
        ModuleTextColor = "Black";
        NodeTipBackColor = "DarkBlue";
        NodeTipForeColor = "White";
        LinkTipBackColor = "DarkRed";
        LinkTipForeColor = "White";
    }
}

public class GridStyle
{
    public Brush BackBrush { get; set; } = "rgba(0,97,204)";
    public Brush MainLineBrush { get; set; } = "rgba(255,255,255,0.5)";
    public Brush SubLineBrush { get; set; } = "rgba(255,255,255,0.2)";
}