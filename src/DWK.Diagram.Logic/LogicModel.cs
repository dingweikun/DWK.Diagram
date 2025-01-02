using Avalonia.Media;
using Northwoods.Go.Models;

namespace DWK.Diagram;

// public class LogicModel : GraphLinksModel<LogicNodeData, Guid, object, LogicLinkData, Guid, string>
public class LogicModel : GraphLinksModel<LogicNodeData, Guid, object, LogicLinkData, Guid, string>
{
    public LogicModel()
    {
        LinkFromKeyProperty = nameof(LogicLinkData.From);
        LinkFromPortIdProperty = nameof(LogicLinkData.FromPort);
        
        LinkToKeyProperty = nameof(LogicLinkData.To);
        LinkToPortIdProperty = nameof(LogicLinkData.ToPort);
    }
}