using DWK.Diagram.ElecModels;
using Northwoods.Go.Models;

namespace DWK.Diagram.Node;

/// <summary>
/// 电气节点模板基类
/// </summary>
public abstract class ElecNodeTemplate<TNodeData> where TNodeData : ElecNodeData, new()
{
    protected TNodeData Sample { get; } = new TNodeData();

    public abstract Northwoods.Go.Node Make();

    #region pre-defined binding function

    protected static Binding BindingDefaultFont() => new Binding("Font", nameof(ElecDiagramTheme.DefaultFont)).OfModel();
    protected static Binding BindingTinyFont() => new Binding("Font", nameof(ElecDiagramTheme.TinyFont)).OfModel();

    protected static Binding BindingLocation() => new Binding("Location").MakeTwoWay();

    protected static Binding BindingLocationText() => new Binding("Text", "Location", loc => loc.ToString());

    protected static Binding[] BindingTag() =>
    [
        new Binding("Text", nameof(ElecNodeData.Tag)),
        new Binding("Font", nameof(ElecDiagramTheme.TagFont)).OfModel(),
        new Binding("Stroke", nameof(ElecDiagramTheme.DefaultTextBrush)).OfModel(),
    ];

    protected static Binding BindingOrientation(bool antiClockwise = false) =>
        new Binding("Angle", nameof(IOrientation.IsVertical), val => val is true ? (antiClockwise ? -90 : 90) : 0);

    #endregion

    #region pre-defined graph object

    protected static TextBlock TagTextBlock() => new TextBlock
        {
            TextAlign = TextAlign.Center, Alignment = Spot.Bottom, AlignmentFocus = new Spot(0.5, 0, 0, -10)
        }
        .Bind(BindingTag());

    protected static TextBlock LocationTextBlock() => new TextBlock
        {
            Alignment = Spot.TopLeft, AlignmentFocus = new Spot(0, 1, 0, 10),
            Background = "blue", Stroke = "white"
        }
        .Bind(BindingLocationText())
        .Bind(BindingTinyFont());

    #endregion

    #region pre-defined port function

    protected Shape DefaultPortShape() => new Shape("Rectangle")
    {
        Height = 8, Width = 8, Fill = "Aqua", StrokeWidth = 0, AlignmentFocus = Spot.Center
    };

    protected object ElecPortSetting(string portName, EPortType type, EPortType target) => new
    {
        Name = portName,
        PortId = portName,
        Cursor = "pointer",
        CustomProperties = new
        {
            _PortType = type,
            _PortTarget = target
        }
    };

    #endregion

    #region pre-defined link validation

    /// <summary>
    /// 限制端口连接数量为1
    /// </summary>
    protected static bool OneLinkValidation(
        Northwoods.Go.Node formNode, GraphObject fromPort, Northwoods.Go.Node toNode, GraphObject toPort, Link link)
    {
        if (formNode.Data is TNodeData && formNode.FindLinksConnected(fromPort.PortId).Any())
            return false;

        if (toNode.Data is TNodeData && toNode.FindLinksConnected(toNode.PortId).Any())
            return false;

        return true;
    }

    #endregion
}