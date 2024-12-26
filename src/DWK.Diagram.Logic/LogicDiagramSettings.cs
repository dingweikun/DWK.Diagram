using Northwoods.Go;

namespace DWK.Diagram;

public class LogicDiagramSettings : BaseDiagramSettings
{
    public Size PortSize { get; set; }

    public Font TagFont { get; set; }

    public Font HeadFont { get; set; }

    public Font PortFont { get; set; }

    public string ModuleBackColor { get; set; }

    public string ModuleTextColor { get; set; }

    public LogicDiagramSettings()
    {
        PortSize = new Size(6, 6);

        TagFont = new Font(FontName, 15, FontWeight.Regular);
        HeadFont = new Font(FontName, 15, FontWeight.Bold);
        PortFont = new Font(FontName, 13, FontWeight.Regular);

        ModuleBackColor = "LightBlue";
        ModuleTextColor = "Black";
    }
}