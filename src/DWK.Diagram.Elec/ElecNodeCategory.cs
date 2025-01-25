namespace DWK.Diagram;

/// <summary>
/// 电气节点类型
/// </summary>
public static class ElecNodeCategory
{
    /// <summary>
    /// 母线
    /// </summary>
    public const string Bus = nameof(Bus);

    /// <summary>
    /// 节点（母线）
    /// </summary>
    public const string BusPoint = nameof(BusPoint);


    public const string Wire = nameof(Wire);

    /// <summary>
    /// 支路
    /// </summary>
    public const string Line = nameof(Line);

    /// <summary>
    /// 刀闸
    /// </summary>
    public const string Switch = nameof(Switch);

    /// <summary>
    /// 断路器/熔断器
    /// </summary>
    public const string Breaker = nameof(Breaker);

    /// <summary>
    /// 变压器
    /// </summary>
    public const string Transformer = nameof(Transformer);

    /// <summary>
    /// 负载
    /// </summary>
    public const string Load = nameof(Load);

    /// <summary>
    /// 发电机（简单）
    /// </summary>
    public const string Generator = nameof(Generator);
    
    /// <summary>
    /// 电动机
    /// </summary>
    public const string Motor = nameof(Motor);
}