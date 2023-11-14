using Northwoods.Go;
using Northwoods.Go.Models;
using Prism.Events;
using System;
using System.Collections.Generic;

using Go = Northwoods.Go;

namespace DWK.Diagram.Models;

public interface IDiagramAgent
{
    Go.Diagram Reset(Go.Diagram diagram);
}

public class DiagramInitEvent : PubSubEvent<IDiagramAgent>
{
}

public class LogicDiagramAgent : IDiagramAgent
{
    public Go.Diagram Reset(Go.Diagram diagram)
    {
        diagram.Model = new LogicDiagramModel
        {
            NodeDataSource = new List<LogicNodeData> {
                new LogicNodeData { Key = Guid.NewGuid() },
                new LogicNodeData { Key = Guid.NewGuid(), Tag = "A" },
                new LogicNodeData { Key = Guid.NewGuid(), Tag = "B" }
             }
        };

        diagram.NodeTemplate = new Node("Horizontal")
        { Background = "#44CCFF" }
            .Add(new TextBlock() { Margin = 12, Stroke = "white", Font = new Font("Segoe UI", 16, FontWeight.Bold) }
            .Bind("Text", "Tag")
        );

        return diagram;
    }
}

public enum SignalTypes
{
    Analog, Digital
}

public class LogicDiagramModel : GraphLinksModel<LogicNodeData, Guid, object, LogicLinkData, Guid, string>
{ }

public class LogicNodeData : LogicDiagramModel.NodeData
{
    public string Tag { get; set; } = "<Logic Node>";
}

public class LogicLinkData : LogicDiagramModel.LinkData
{
    public SignalTypes SignalType { get; set; }
}