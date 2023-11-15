using DWK.Diagram.Base;

namespace DWK.Diagram.Logic;

public class LogicDiagramAgent : IDiagramAgent
{
    public GoDiagram Reset(GoDiagram diagram)
    {
        //diagram.Model = new LogicDiagramModel
        //{
        //    NodeDataSource = new List<LogicNodeData>
        //    {
        //        new() { Key = Guid.NewGuid(), Tag = "中文测试" },
        //        new() { Key = Guid.NewGuid(), Tag = "A" },
        //        new() { Key = Guid.NewGuid(), Tag = "B" }
        //    }
        //};

        //diagram.NodeTemplate = CreateLogicNodeTemplate("lightyellow", "Arial");

        diagram.Add(new Part(PanelType.Position) { Background = "lightyellow" }
            .Add(new Panel(PanelType.Vertical) { Background = "teal" }
                .Add(new TextBlock("this is text")))
            .Add(new Shape("rectangle") { Fill = null, StrokeWidth = 1, Stroke = "black", Width = 200, Height = 200 })
             );





        return diagram;
    }

    private static Node CreateLogicNodeTemplate(string backColor, string fontFamily)
    {
        var template = new Node(PanelLayoutVertical.Instance) { LocationSpot = Spot.Center };

        var rect = new Shape("Rectangle") { Fill = backColor };
        template.Add(rect);

        var tag = new TextBlock { Margin = new(4, 0), Font = new Font(fontFamily, 16), TextAlign = TextAlign.Center, Editable = true };
        tag.Bind(nameof(TextBlock.Text), nameof(LogicNodeData.Tag));
        template.Add(tag);

        return template;
    }
}