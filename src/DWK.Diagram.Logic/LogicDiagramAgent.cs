using DWK.Diagram.Base;

namespace DWK.Diagram.Logic;

public class LogicDiagramAgent : IDiagramAgent
{
    public GoDiagram Reset(GoDiagram diagram)
    {
        diagram.Reset();

        //diagram.NodeTemplateMap = BuildNodeTemplateMap(LogicMockData.ModuleDefines);

        return diagram;
    }

    //private static IDictionary<string, Part> BuildNodeTemplateMap(IEnumerable<LogicModuleDefine> moduleDefines)
    //{
    //    var templateDict = new Dictionary<string, Part>();
    //    foreach (var def in moduleDefines)
    //    {
    //        templateDict.Add(def.Name, BuildNodeTemplate(def));
    //    }
    //    return templateDict;
    //}

    //private static Part BuildNodeTemplate(LogicModuleDefine define)
    //{
    //    var inPorts = define.Ports.Where(p => p.IsInput).Select(p => p.PortId).ToArray();
    //    var outPorts = define.Ports.Where(p => !p.IsInput).Select(p => p.PortId).ToArray();
    //    var portRows = Math.Max(inPorts.Length, outPorts.Length);

    //    var table = new Part("Table")
    //        .Add(new Shape("RoundedRectangle")
    //        {
    //            Fill = "lightblue",
    //            Stretch = Stretch.Fill,
    //            Row = 0,
    //            RowSpan = portRows + 2,
    //            Column = 1,
    //            ColumnSpan = 2,
    //        })
    //        .Add(new TextBlock(define.Name)
    //        {
    //            Font = new Font("Arial", 18, FontWeight.Bold),
    //            MinSize = new Size(80, 0),
    //            TextAlign = TextAlign.Center,
    //            Wrap = Wrap.DesiredSize,
    //            Margin = new Margin(16, 16, 8, 16),
    //            Row = 0,
    //            Column = 1,
    //            ColumnSpan = 2,
    //        });

    //    var font = new Font("Arial", 16);
    //    table.Add(BuildPortGraphicObjects(portRows, true, inPorts, font));
    //    table.Add(BuildPortGraphicObjects(portRows, false, outPorts, font));
    //    return table;
    //}

    private static IList<GraphObject> BuildPortGraphicObjects(int maxPortCount, bool isLeft, IList<string> portNames, Font font)
    {
        var graphObjects = new List<GraphObject>();

        int shapeColumn = isLeft ? 0 : 3;
        int textColumn = isLeft ? 1 : 2;
        var textMargin = isLeft ? new Margin(8, 16, 8, 4) : new Margin(8, 4, 8, 16);
        var textAlign = isLeft ? TextAlign.Left : TextAlign.Right;
        var linkSpot = isLeft ? Spot.Left : Spot.Right;

        int rowOffset = (maxPortCount - portNames.Count) / 2 + 1;
        for (int i = 0; i < portNames.Count; i++)
        {
            var shape = new Shape("Rectangle")
            {
                Row = rowOffset + i,
                Column = shapeColumn,
                PortId = portNames[i],
                Fill = "Black",
                DesiredSize = new Size(8, 8),
                ToSpot = linkSpot,
                ToLinkable = isLeft,
                FromSpot = linkSpot,
                FromLinkable = !isLeft
            };

            var text = new TextBlock()
            {
                Row = rowOffset + i,
                Column = textColumn,
                Margin = textMargin,
                Wrap = Wrap.None,
                Text = portNames[i],
                Font = font,
                TextAlign = textAlign,
                Stretch = Stretch.Horizontal,
                VerticalAlignment = Spot.Center,
            };

            if (i + 1 == maxPortCount)
            {
                AddMarginBottom(text, 16);
                AddMarginBottom(shape, 16);
            }
            graphObjects.Add(shape);
            graphObjects.Add(text);
        }

        return graphObjects;
    }

    private static void AddMarginBottom(GraphObject graph, int addBottom)
    {
        var margin = graph.Margin;
        margin.Bottom += addBottom;
        graph.Margin = margin;
    }
}