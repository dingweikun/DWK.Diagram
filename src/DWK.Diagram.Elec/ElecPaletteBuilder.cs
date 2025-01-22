using DWK.Diagram.Node;

namespace DWK.Diagram;

public class ElecPaletteBuilder
{
    public void BuildPalette(Northwoods.Go.Diagram diagram)
    {
        // 创建模型
        diagram.Model = new ElecModel
        {
            SharedData = new ElecDiagramTheme(),

            NodeDataSource = new List<ElecNodeData>
            {
                new ElecNodeData(),
                new BusNodeData(),
                new BusPointNodeData(),
                new LineNodeData(),
                new TransfNodeData(),
                new SwitchNodeData(),
            }
        };

        // 设置模板
        diagram.NodeTemplateMap = MakeNodeTemplateMap();
    }

    #region private methods

    private static Dictionary<string, Part> MakeNodeTemplateMap() => new()
    {
        // 未定义
        {
            string.Empty, TemplateNode().Add(new Shape("XLine") { Stretch = Stretch.Fill })
        },
        // 母线
        {
            ElecNodeCategory.Bus,
            TemplateNode().Add(new Shape
            {
                Width = 30,
                GeometryString = IconGeometry.Bus
            })
        },
        // 节点(母线)
        {
            ElecNodeCategory.BusPoint,
            TemplateNode().Add(new Shape
            {
                Width = 20, Height = 20,
                GeometryString = IconGeometry.BusPoint
            })
        },
        // 刀闸
        {
            ElecNodeCategory.Switch,
            TemplateNode().Add(new Shape
            {
                Width = 17, Height = 30,
                GeometryString = IconGeometry.SwitchOpen
            })
        },
        // 支路
        {
            ElecNodeCategory.Line,
            TemplateNode().Add(new Shape
            {
                Width = 30, Height = 12,
                GeometryString = IconGeometry.BranchLine
            })
        },
        // 变压器
        {
            ElecNodeCategory.Transformer,
            TemplateNode().Add(new Shape
            {
                Width = 18.86, Height = 30,
                GeometryString = IconGeometry.Transformer
            })
        },
        // // 负载
        // { ElecNodeCategory.Load, new LoadNodeTemplate().Make() },
    };

    private static Northwoods.Go.Node TemplateNode() => new Northwoods.Go.Node(PanelLayoutSpot.Instance)
        .Add(new Shape("Rectangle") { Width = 40, Height = 40, Fill = "#e5e5e5", StrokeWidth = 1, Stroke = "#bdbdbd" });

    #endregion
}