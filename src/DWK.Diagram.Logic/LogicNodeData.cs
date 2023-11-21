namespace DWK.Diagram.Logic;




public record struct LogicPortDefine
{
    public string Name { get; set; }
}


public readonly record struct LogicModuleDefine
{
    public string Category { get; init; }

    public string Description { get; init; }
}



public class LogicNodeData : LogicDiagramModel.NodeData
{
    public string Tag { get; set; } = "<Logic Node>";
}

public class LogicNodeStyle
{
}

///-------------------
///
//public static class LogicMockData
//{
//    public static LogicModuleDefine[] ModuleDefines { get; } = new[]{
//        new LogicModuleDefine("Add"){
//            Ports = new LogicPortDefine[]
//            {
//                new LogicPortDefine{PortId="X1", IsInput=true, Type=SignalTypes.Digital},
//                new LogicPortDefine{PortId="X2", IsInput=true, Type=SignalTypes.Digital},
//                new LogicPortDefine{PortId="Y", IsInput=false, Type=SignalTypes.Digital},
//            }
//        },
//        new LogicModuleDefine("Sub"){
//            Ports = new LogicPortDefine[]
//            {
//                new LogicPortDefine{PortId="X1", IsInput=true, Type=SignalTypes.Digital},
//                new LogicPortDefine{PortId="X2", IsInput=true, Type=SignalTypes.Digital},
//                new LogicPortDefine{PortId="Y", IsInput=false, Type=SignalTypes.Digital},
//            }
//        },
//    };
//}