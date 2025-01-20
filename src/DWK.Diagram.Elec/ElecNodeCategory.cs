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
    public const string Break = nameof(Break);

    /// <summary>
    /// 变压器
    /// </summary>
    public const string Transformer = nameof(Transformer);

    /// <summary>
    /// 负载
    /// </summary>
    public const string Load = nameof(Load);
}