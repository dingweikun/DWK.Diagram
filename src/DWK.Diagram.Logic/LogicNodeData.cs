namespace DWK.Diagram.Logic;

public readonly record struct LogicPortDefine
{
    public string PortId { get; init; }
    public bool IsInput { get; init; }
    public SignalTypes Type { get; init; }
}

public record LogicModuleDefine(string Name)
{
    public LogicPortDefine[] Ports { get; init; } = Array.Empty<LogicPortDefine>();
}

public interface ILogicModuleData
{
    public Guid Key { get; }
    public string Tag { get; set; }
}

public class LogicNodeData : LogicDiagramModel.NodeData, ILogicModuleData
{
    public string Tag { get; set; } = "<Logic Node>";
}

public class LogicNodeStyle
{
}

///-------------------
///
public static class LogicMockData
{
    public static LogicModuleDefine[] ModuleDefines { get; } = new[]{
        new LogicModuleDefine("Add"){
            Ports = new LogicPortDefine[]
            {
                new LogicPortDefine{PortId="X1", IsInput=true, Type=SignalTypes.Digital},
                new LogicPortDefine{PortId="X2", IsInput=true, Type=SignalTypes.Digital},
                new LogicPortDefine{PortId="Y", IsInput=false, Type=SignalTypes.Digital},
            }
        },
        new LogicModuleDefine("Sub"){
            Ports = new LogicPortDefine[]
            {
                new LogicPortDefine{PortId="X1", IsInput=true, Type=SignalTypes.Digital},
                new LogicPortDefine{PortId="X2", IsInput=true, Type=SignalTypes.Digital},
                new LogicPortDefine{PortId="Y", IsInput=false, Type=SignalTypes.Digital},
            }
        },
    };
}