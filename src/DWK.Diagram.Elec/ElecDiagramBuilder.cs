using DWK.Diagram.Node;
using Northwoods.Go.Models;

namespace DWK.Diagram;

public static class ElecNodeCategory
{
    public const string Bus = nameof(Bus);
}

public class ElecDiagramBuilder : IDiagramBuilder
{
    public void BuildDiagram(Northwoods.Go.Diagram diagram)
    {
      
        diagram.Model = new ElecModel
        {
            NodeDataSource = new List<ElecNodeData>
            {
                new BusNodeData { Key = Guid.NewGuid() },
            },
        };

        diagram.NodeTemplate = BusNodeTemplate.Make();
        // diagram.NodeTemplateMap = MakeNodeTemplateMap();

        //TODO: Test.....
        diagram.Grid.Visible = true;
        diagram.ToolManager.ClickCreatingTool.ArchetypeNodeData = new BusNodeData();
    }

    #region private methods

    private static Dictionary<string, Part> MakeNodeTemplateMap() => new()
    {
        { ElecNodeCategory.Bus, BusNodeTemplate.Make() }
    };

    #endregion
}

#region Model Definitions

public class ElecModel : GraphLinksModel<ElecNodeData, Guid, object, ElecLinkData, Guid, string>
{
    public ElecModel()
    {
        // LinkFromKeyProperty = nameof(ElecLinkData.From);
        // LinkFromPortIdProperty = nameof(ElecLinkData.FromPort);
        //
        // LinkToKeyProperty = nameof(ElecLinkData.To);
        // LinkToPortIdProperty = nameof(ElecLinkData.ToPort);
    }
}

public class ElecNodeData : ElecModel.NodeData
{
    public string Tag { get; set; } = string.Empty;
}

public class ElecLinkData : ElecModel.LinkData
{
}

public class ElecSharedData
{
}

#endregion