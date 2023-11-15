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


        // var portShape = new Shape("Rectangle")
        // {
        //     Position = new Point()
        //     Fill = "black", Stroke = null, StrokeWidth = 0, DesiredSize = new Size(8, 8),
        // };
        //
        // var portText = new TextBlock("this port A 中文")
        // {
        //     Font = new Font("Arial", 7, FontStyle.Regular, FontUnit.Point),
        //     Margin = new Margin(0, 8)
        // };
        //
        // var port = new Panel(PanelLayoutHorizontal.Instance)
        //     {
        //         Alignment = new Spot(0, 0),
        //         AlignmentFocus = new Spot(0,0.5, 8,0)
        //     }
        //     .Add(portShape)
        //     .Add(portText);
        //
        //
        // var node = new Part("Spot") { Background = "lightblue" }
        //     .Add(new Shape("Rectangle") { Height = 100, Width = 200, Fill = "teal" });
        //
        // node.Add(port);
        //
        // diagram.Add(node);

        var iport = Enumerable.Range(1, 5).Select(i => $"In-port-{i}").ToArray();
        var oport = Enumerable.Range(1, 3).Select(i => $"out-port-{i}").ToArray();


        var font = new Font("Arial", 16);

        // 计算 Table
        var nrow = Math.Max(iport.Length, oport.Length);


        var table = new Part("Table")
           
            .Add(new TextBlock("Title string")
            {
                Font = font, Background = "yellow",
                MinSize = new Size(100, 0), TextAlign = TextAlign.Center,
                Margin = new Margin(16, 16, 8, 16),
                Row = 0, Column = 1, ColumnSpan = 2
            });

        {
            var ioffset = (nrow - iport.Length) / 2;
            for (int i = 1; i <= iport.Length; i++)
            {
                var rect = new Shape("Rectangle")
                {
                    Fill = "black", DesiredSize = new Size(8, 8),
                    Column = 0, Row = i + ioffset + 1,
                    Margin = i != nrow + 1 ? new Margin(0) : new Margin(0, 0, 20 - 4, 0)
                };

                var text = new TextBlock(iport[i-1])
                {
                    Font = font,
                    Alignment = Spot.Left,
                    Column = 1, Row = i + ioffset + 1, Background = "lightgreen",
                    Margin = i != nrow + 1 ? new Margin(4, 10, 4, 0) : new Margin(4, 10, 20, 0)
                };

                table.Add(rect);
                table.Add(text);
            }
        }

        {
            var ooffset = (nrow - oport.Length) / 2;
            for (int i = 1; i <= oport.Length; i++)
            {
                var rect = new Shape("Rectangle")
                {
                    Fill = "black", DesiredSize = new Size(8, 8),
                    Column = 3, Row = i + ooffset + 1,
                    Margin = i != nrow + 1 ? new Margin(0) : new Margin(0, 0, 20 - 4, 0)
                };

                var text = new TextBlock(oport[i-1])
                {
                    Font = font,
                    Alignment = Spot.Right,
                    Column = 2, Row = i + ooffset + 1, Background = "lightgreen",
                    Margin = i != nrow + 1 ? new Margin(4, 0, 4, 10) : new Margin(4, 0, 20, 10)
                };

                table.Add(rect);
                table.Add(text);
            }
        }

        table.Add(new Shape("Rectangle")
        {
            Stroke = "red",
            Fill = null, Stretch = Stretch.Fill,
            Row = 0, RowSpan = nrow +2,
            Column = 1, ColumnSpan = 2
        });

        diagram.Add(table);

        //     diagram.Add(
        //         // all Parts are Panels
        //         new Part("Table")
        //             .Add(new Shape("Rectangle")
        //             {
        //                 Fill = "yellow",
        //                 Row = 0, RowSpan = 2,
        //                 Column = 0, ColumnSpan = 3, Stretch = Stretch.Fill
        //             })
        //             .Add(
        //                 new TextBlock("Row 0\nCol0")
        //                     { Row = 0, Column = 0, Margin = 2, Background = "lightgray", Font = font },
        //                 new TextBlock("Row 0\nCol1")
        //                     { Row = 0, Column = 1, Margin = 2, Background = "lightgray", Font = font },
        //                 new TextBlock("Row 1\nCol0")
        //                     { Row = 1, Column = 0, Margin = 2, Background = "lightgray", Font = font },
        //                 new TextBlock("Row 1\nCol2")
        //                     { Row = 1, Column = 2, Margin = 2, Background = "lightgray", Font = font }
        //             )
        //     );
        return diagram;
    }

    private static Node CreateLogicNodeTemplate(string backColor, string fontFamily)
    {
        var template = new Node(PanelLayoutVertical.Instance) { LocationSpot = Spot.Center };

        var rect = new Shape("Rectangle") { Fill = backColor };
        template.Add(rect);

        var tag = new TextBlock
            { Margin = new(4, 0), Font = new Font(fontFamily, 16), TextAlign = TextAlign.Center, Editable = true };
        tag.Bind(nameof(TextBlock.Text), nameof(LogicNodeData.Tag));
        template.Add(tag);

        return template;
    }
}