using DWK.Diagram.Node;
using Northwoods.Go.Layouts;
using Northwoods.Go.Models;

namespace DWK.Diagram;

public class ElecPaletteBuilder
{
    public void BuildPalette(Northwoods.Go.Diagram diagram)
    {
        diagram.Layout = new GridLayout()
        {
            Arrangement = GridArrangement.LeftToRight
        };
        


        // 创建模型
        diagram.Model = new ElecModel
        {
            SharedData = new ElecDiagramTheme(),

            NodeDataSource = new List<ElecNodeData>
            {
                // new BusNodeData(),
                new LineNodeData(),
                new TransfNodeData(),
                new SwitchNodeData(),
            }
        };

        // 设置模板
        // diagram.NodeTemplate = MakeDefaultNodeTemplate();
        diagram.NodeTemplateMap = MakeNodeTemplateMap();
    }

    #region private methods

    private static Dictionary<string, Part> MakeNodeTemplateMap() => new()
    {
        // // 未定义
        // { string.Empty, MakeDefaultNodeTemplate() },
        // // 母线
        // { ElecNodeCategory.Bus, BusNodeTemplate.Make() },
        // 刀闸
        {
            ElecNodeCategory.Switch,
            TemplateNode().Add(new Shape
            {
                Width = 17, Height = 30,
                GeometryString = "M 8.4366961,76.68331 1.92,83.2 M 0,75.17654 H 3.84 M 1.92,71.68 v 3.84 m 0,7.68 v 3.84"
            })
        },
        {
            // 支路
            ElecNodeCategory.Line,
            TemplateNode().Add(new Shape
            {
                Width = 30, Height = 12,
                GeometryString = "m 0,130 h 2 l 2,4 v -8 l 4,8 v -8 l 4,8 v -8 l 4,8 v -8 l 2,4 h 2"
            })
        },
        {
            // 变压器
            ElecNodeCategory.Transformer,
            TemplateNode().Add(new Shape
            {
                Width = 18.86, Height = 30,
                GeometryString =
                    "M 10.766904,113.664 A 5.1349044,5.1349044 0 0 1 5.632,118.79891 5.1349044,5.1349044 0 0 1 0.49709558,113.664 5.1349044,5.1349044 0 0 1 5.632,108.5291 a 5.1349044,5.1349044 0 0 1 5.134904,5.1349 z m 0,-6.656 A 5.1349044,5.1349044 0 0 1 5.632,112.14291 5.1349044,5.1349044 0 0 1 0.49709558,107.008 5.1349044,5.1349044 0 0 1 5.632,101.8731 a 5.1349044,5.1349044 0 0 1 5.134904,5.1349 z",
            })
        },
        // // 负载
        // { ElecNodeCategory.Load, new LoadNodeTemplate().Make() },
    };

    private static Northwoods.Go.Node TemplateNode() => new Northwoods.Go.Node(PanelLayoutSpot.Instance)
        .Add(new Shape("Rectangle") { Width = 40, Height = 40, Fill = "#e5e5e5", StrokeWidth = 1, Stroke = "#bdbdbd" });

    #endregion
}