using System.Diagnostics.CodeAnalysis;

namespace DWK.Diagram.ElecModels;

public record EPort(EPortType Type, EPortType Target);

public enum EPortType
{
    NULL = 0,
    NODE = 1,
    LINK = 2,
    INFO = 4,
    All = NODE | LINK | INFO
}


/// <summary>
/// 电气程序节点类模型的类型（NTYP）
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum ENodeType
{
    PQ = 0,
    PV = 2,
    BALANCE = 3,
    LOAD = 4
}

/// <summary>
/// 电气程序节点类模型接口
/// </summary>
public interface IENode
{
    /// <summary>
    /// 节点类型
    /// </summary>
    ENodeType Type { get; }
}

/// <summary>
/// 电气程序支路类模型接口
/// </summary>
public interface IELink
{
}

/// <summary>
/// 电气程序信息类模型接口
/// </summary>
public interface IEInfo
{
}