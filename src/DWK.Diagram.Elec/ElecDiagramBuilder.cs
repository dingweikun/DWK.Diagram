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

            NodeDataSource = new List<ElecNodeData>
            {
                // new BusNodeData { Key = Guid.NewGuid() },
                new WireNodeData { Key = Guid.NewGuid(), Angle = 45 },
                new WireNodeData { Key = Guid.NewGuid(), Angle = 90 },
                new SwitchNodeData {Key = Guid.NewGuid(), Opened = true, IsVertical = true},
                new SwitchNodeData {Key = Guid.NewGuid(), Opened = false},
            },
        };

        // 设置模板
        // diagram.NodeTemplate = MakeDefaultNodeTemplate();
        diagram.NodeTemplateMap = MakeNodeTemplateMap();
        diagram.LinkTemplate = ElecLinkTemplate.Make();

        //TODO: Test.....
        
        diagram.Grid.Visible = true;
        diagram.ToolManager.ClickCreatingTool.ArchetypeNodeData = new SwitchNodeData();

        diagram.Add(
            new Northwoods.Go.Node(PanelLayoutSpot.Instance)
                .Add(
                    new Shape("Ellipse") { Width = 100, Height = 100, Fill = "red", StrokeWidth = 0 },
                    new Shape("Ellipse") { Width = 80, Height = 80, Fill = "red", Stroke = "white", StrokeWidth = 8 },
                    new Shape
                    {
                        Height = 36, Width = 36, Fill = "white", StrokeWidth = 0,
                        GeometryString =
                            "F M 8.3923339e-8,1075.287 H 19.300024 v -44.8399 c 0,-10.1579 -1.741356,-25.1046 -2.757146,-35.26247 h 0.580451 c 7.711934,23.76337 16.610317,47.01597 25.249656,70.37977 h 12.334601 c 8.779043,-23.306 17.116734,-46.7943 25.249655,-70.37977 h 0.725565 c -1.160903,10.15787 -2.757146,25.10457 -2.757146,35.26247 v 44.8399 H 97.51591 V 967.75828 H 73.717384 C 64.84767,990.67797 56.773927,1016.6971 49.338407,1038.7185 H 48.612842 C 41.475761,1014.8228 32.580843,991.94451 23.798526,967.75828 H 8.3923339e-8 Z"
                    })
        );
        
        diagram.Add(
            new Northwoods.Go.Node(PanelLayoutSpot.Instance)
                .Add(
                    new Shape("Ellipse") { Width = 100, Height = 100, Fill = "red", StrokeWidth = 0 },
                    new Shape("Ellipse") { Width = 80, Height = 80, Fill = "red", Stroke = "white", StrokeWidth = 8 }
                )
        );

        diagram.Add(
            new Northwoods.Go.Node(PanelLayoutSpot.Instance)
                .Add(
                    new Shape("Ellipse") { Width = 100, Height = 100, Fill = "red", StrokeWidth = 0 },
                    new Shape("Ellipse") { Width = 80, Height = 80, Fill = "red", Stroke = "white", StrokeWidth = 8 },
                    new Shape
                    {
                        Stroke = "white", StrokeWidth = 8,
                        GeometryString = "M 0,0 L 0,-40 M 0,0 L 34.64,20 M 0,0 L -34.64,20 M0,40"
                    }
                    // new Shape
                    // {
                    //     Height = 40, Width = 40, Fill = "white", StrokeWidth = 0,
                    //     GeometryString =
                    //         "F M 8.3923339e-8,1075.287 H 19.300024 v -44.8399 c 0,-10.1579 -1.741356,-25.1046 -2.757146,-35.26247 h 0.580451 c 7.711934,23.76337 16.610317,47.01597 25.249656,70.37977 h 12.334601 c 8.779043,-23.306 17.116734,-46.7943 25.249655,-70.37977 h 0.725565 c -1.160903,10.15787 -2.757146,25.10457 -2.757146,35.26247 v 44.8399 H 97.51591 V 967.75828 H 73.717384 C 64.84767,990.67797 56.773927,1016.6971 49.338407,1038.7185 H 48.612842 C 41.475761,1014.8228 32.580843,991.94451 23.798526,967.75828 H 8.3923339e-8 Z"
                    // }
                )
        );

        diagram.Add(new Northwoods.Go.Node(PanelLayoutSpot.Instance)
            {
                LocationElementName = "WIRE_SHAPE"
            }
            .Add(
                new Shape("Rectangle")
                {
                    Name = "WIRE_SHAPE", Fill = "transparent", Width = 200, Height = 20, StrokeWidth = 0
                },
                new Shape
                {
                    Stroke = "green", StrokeWidth = 6, StrokeJoin = LineJoin.Bevel,
                    GeometryString = "M0,0 L20,0 L30,20 L50,-20 L70,20 L90,-20 L110,20 L130,-20 L150,20 L170,-20 L180,0 L200,0"
                },
                new Shape("Rectangle")
                    { Height = 8, Width = 8, Fill = "blue", StrokeWidth = 0, Alignment = new Spot(0, 0.5), AlignmentFocus = Spot.Center },
                new Shape("Rectangle")
                    { Height = 8, Width = 8, Fill = "blue", StrokeWidth = 0, Alignment = new Spot(1, 0.5), AlignmentFocus = Spot.Center }
            )
        );
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
        { ElecNodeCategory.Bus, BusNodeTemplate.Make() },
        // 刀闸
        { ElecNodeCategory.Switch, SwitchNodeTemplate.Instance }
    };

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
    public string Tag { get; set; }
}

public class BranchNodeData : ElecNodeData;

public class LoadNodeData : ElecNodeData;

public class ElecLinkData : ElecModel.LinkData
{
    public ElecLinkData()
    {
    }
}

#endregion