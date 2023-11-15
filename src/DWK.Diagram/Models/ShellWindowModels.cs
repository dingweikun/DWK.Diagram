using Prism.Events;

namespace DWK.Diagram.Models;

public static class RegionNames
{
    public const string SidePanelRegion = nameof(SidePanelRegion);
    public const string DiagramRegion = nameof(DiagramRegion);
}

public class ShiftSidePanelEvent : PubSubEvent<string>
{
}

