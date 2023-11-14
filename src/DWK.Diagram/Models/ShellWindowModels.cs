using Prism.Events;

namespace DWK.Diagram.Models;

public static class SidePanelTitles
{
    public const string PageLsit = "Page List";
    public const string ModuleLsit = "Module List";
    public const string ModuleLibrary = "Module Library";
}

public static class RegionNames
{
    public const string SidePanelRegion = nameof(SidePanelRegion);
    public const string DiagramRegion = nameof(DiagramRegion);
}

public class ShiftSidePanelEvent : PubSubEvent<string>
{
}