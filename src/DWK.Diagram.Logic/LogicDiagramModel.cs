namespace DWK.Diagram.Logic;

public enum SignalTypes
{
    Analog,
    Digital,
}

public class LogicDiagramModel : GraphLinksModel<LogicNodeData, Guid, object, LogicLinkData, Guid, string>
{
}