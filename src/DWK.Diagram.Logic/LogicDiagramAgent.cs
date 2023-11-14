using Avalonia.Controls.Primitives;
using Avalonia.Media;
using DWK.Diagram.Base;
using HarfBuzzSharp;
using Font = Northwoods.Go.Font;
using PathFigure = Avalonia.Media.PathFigure;

namespace DWK.Diagram.Logic;

public class LogicDiagramAgent : IDiagramAgent
{
    public GoDiagram Reset(GoDiagram diagram)
    {
        diagram.Model = new LogicDiagramModel
        {
            NodeDataSource = new List<LogicNodeData>
            {
                new() { Key = Guid.NewGuid(), Tag = "X" },
                new() { Key = Guid.NewGuid(), Tag = "A" },
                new() { Key = Guid.NewGuid(), Tag = "B" }
            }
        };

        diagram.NodeTemplate = CreateLogicNodeTemplate("lightyellow", "Noto Sans");


        return diagram;
    }

    private static Node CreateLogicNodeTemplate(string backColor, string fontFamily)
    {
        var template = new Node(PanelLayoutSpot.Instance);

        var rect = new Shape("Rectangle") { Fill = backColor };
        template.Add(rect);

        var tag = new TextBlock { Margin = 12, Font = new Font(fontFamily, 16) };
        tag.Bind(nameof(TextBlock.Text), nameof(LogicNodeData.Tag));
        template.Add(tag);

        return template;
    }
}