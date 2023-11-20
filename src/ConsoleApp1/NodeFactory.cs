using System.Text.Json.Serialization;

namespace ConsoleApp1;

public class NodeFactory
{
}

//-------------------------------------------------------------------------------------------------------------------------

public class LogicNodeData
{
    public string Category { get; init; } = string.Empty;

    public Guid Key { get; init; }

    public string Tag { get; set; } = "<tag>";

    public IDictionary<string, Guid[]> Ports { get; init; } = new Dictionary<string, Guid[]>();

    public dynamic? Params { get; set; } = null;
}

public class LogicNodeDef
{
    public string Category { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public IDictionary<string, PortDef> Ports { get; init; } = new Dictionary<string, PortDef>();

    public IDictionary<string, ParamDef> Params { get; init; } = new Dictionary<string, ParamDef>();
}

public record class ParamDef
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ParamType Type { get; set; } = ParamType.Double;

    public string Description { get; set; } = string.Empty;

    public ValueType Default { get; set; } = double.NaN;
}

public record class PortDef
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SignalType Type { get; init; } = SignalType.Any;
    public bool IsInput { get; set; } = true;
    public int LinkLimit { get; set; } = 0;
}

public enum SignalType
{
    Any,

    Analog,

    Digital
}

public enum ParamType
{
    Double,
    Bool,
    Int,
}