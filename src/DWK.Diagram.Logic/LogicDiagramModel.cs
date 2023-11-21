using System.Text.Json.Serialization;

namespace DWK.Diagram.Logic;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SignalTypes
{
    Any,
    Analog,
    Digital,
}

public class LogicDiagramModel : GraphLinksModel<LogicNodeData, Guid, object, LogicLinkData, Guid, string>
{
}