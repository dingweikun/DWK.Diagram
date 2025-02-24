using DWK.Diagram.ElecModels;
using DWK.Diagram.Node;
using Northwoods.Go.Models;

namespace DWK.Diagram;

public class ElecDiagramBuilder : IDiagramBuilder
{
    public void BuildDiagram(Northwoods.Go.Diagram diagram)
    {
        // 创建模型
        diagram.Model = new ElecModel
        {
            SharedData = new ElecDiagramTheme(),
            NodeDataSource = new List<ElecNodeData>()
        };

        // 设置模板
        // diagram.NodeTemplate = MakeDefaultNodeTemplate();
        diagram.NodeTemplateMap = MakeNodeTemplateMap();
        diagram.LinkTemplate = ElecLinkTemplate.Make();

        // 设置连接规则（通用）
        diagram.ToolManager.LinkingTool.LinkValidation = GeneralLinkValidation;
        // diagram.ToolManager.RelinkingTool.LinkValidation = GeneralLinkValidation;

        // 功能设置
        diagram.Grid.Visible = true;
        diagram.UndoManager.IsEnabled = true;
        diagram.AnimationManager.IsEnabled = false;
        // diagram.ToolManager.ClickCreatingTool.ArchetypeNodeData = new SwitchNodeData();
    }

    #region private methods

    /// <summary>
    /// 创建默认/未定义节点模板
    /// </summary>
    private static Northwoods.Go.Node MakeDefaultNodeTemplate() => new Northwoods.Go.Node(PanelLayoutSpot.Instance)
        .Add(
            new Shape("Rectangle")
                { Width = 100, Height = 100, Fill = "black", Stroke = "red", StrokeWidth = 2 },
            new Shape
                { Stroke = "red", StrokeWidth = 2, GeometryString = "M0,0 L100,100 M100,0 L0,100" },
            new TextBlock("Undefined\nNode")
                    { Stroke = "White", TextAlign = TextAlign.Center, Alignment = new Spot(0.5, 0), AlignmentFocus = new Spot(0.5, 0) }
                .Bind(new Binding("Font", nameof(ElecDiagramTheme.DefaultFont)).OfModel()),
            new TextBlock
                    { Stroke = "White", TextAlign = TextAlign.Center, Alignment = new Spot(0.5, 1), AlignmentFocus = new Spot(0.5, 1) }
                .Bind(new Binding("Font", nameof(ElecDiagramTheme.DefaultFont)).OfModel())
                .Bind("Text", "Category")
        );

    /// <summary>
    /// 创建各种电气节点模板
    /// </summary>
    private static Dictionary<string, Part> MakeNodeTemplateMap() => new()
    {
        // 未定义
        { string.Empty, MakeDefaultNodeTemplate() },
        // 母线
        { ElecNodeCategory.Bus, new BusNodeTemplate().Make() },
        // 节点（母线）
        { ElecNodeCategory.BusPoint, new BusPointNodeTemplate().Make() },
        // 刀闸
        { ElecNodeCategory.Switch, new SwitchNodeTemplate().Make() },
        // 支路
        { ElecNodeCategory.Line, new LineNodeTemplate().Make() },
        // 变压器
        { ElecNodeCategory.Transformer, new TransfNodeTemplate().Make() },
        // 负载
        { ElecNodeCategory.Load, new LoadNodeTemplate().Make() },
        // 发电机（简单）
        { ElecNodeCategory.Generator, new GeneratorNodeTemplate().Make() },
        // 电动机
        { ElecNodeCategory.Motor, new MotorNodeTemplate().Make() },
        // 断路器
        { ElecNodeCategory.Breaker, new BreakerNodeTemplate().Make() },
    };


    private bool GeneralLinkValidation(Northwoods.Go.Node formNode, GraphObject fromPort, Northwoods.Go.Node toNode, GraphObject toPort, Link link)
    {
        // 限制重复连接
        if (formNode.FindNodesConnected().Contains(toNode)) return false;
        if (toNode.FindNodesConnected().Contains(formNode)) return false;

        // 端口连接规则
        if (fromPort["_PortType"] is not EPortType fromType ||
            fromPort["_PortTarget"] is not EPortType fromTarget ||
            toPort["_PortType"] is not EPortType toType ||
            toPort["_PortTarget"] is not EPortType toTarget)
            return false;

        return (fromType & toTarget) > 0 && (fromTarget & toType) > 0;
    }

    #endregion
}

#region Model Definitions

public class ElecModel : GraphLinksModel<ElecNodeData, Guid, object, ElecLinkData, Guid, string>
{
    public ElecModel()
    {
        LinkFromKeyProperty = nameof(ElecLinkData.From);
        LinkFromPortIdProperty = nameof(ElecLinkData.FromPort);

        LinkToKeyProperty = nameof(ElecLinkData.To);
        LinkToPortIdProperty = nameof(ElecLinkData.ToPort);
    }
}

public class ElecNodeData : ElecModel.NodeData
{
    public string Tag { get; set; } = string.Empty;
}

public class ElecLinkData : ElecModel.LinkData
{
    public ElecLinkData()
    {
    }
}

#endregion