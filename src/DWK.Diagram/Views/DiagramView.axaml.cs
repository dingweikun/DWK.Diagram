using System.Collections.Generic;
using Avalonia.Interactivity;

namespace DWK.Diagram.Views;

public partial class DiagramView : UserControl
{
    public DiagramView()
    {
        InitializeComponent();

        var builder = new LogicDiagramBuilder()
            .WithSettings(settings =>
            {
                // settings.LinkColor = "rgba(255,255,255)";
                // settings.ModuleBackColor = "rgba(0,97,204)";
                // settings.ModuleTextColor = "rgba(255,255,255)";
                // settings.NodeTipBackColor = "rgba(255,0,255)";
                // settings.NodeTipForeColor = "rgba(255,255,255)";
                // settings.LinkTipBackColor = "rgba(255,255,0)";
                // settings.LinkTipForeColor = "rgba(255,255,255)";
            });

        //builder.BuildDiagram(PART_DiagramControl.Diagram);

        var esbuilder = new ElecDiagramBuilder();
        esbuilder.BuildDiagram(PART_DiagramControl.Diagram);


        // Testing.Setup3(PART_DiagramControl.Diagram);
        // Testing.Setup2(PART_DiagramControl.Diagram);
        //Testing.Setup0(PART_DiagramControl.Diagram);
        // Testing.Setup(PART_DiagramControl.Diagram);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var b = PART_DiagramControl.Diagram.Grid.Background.ToString();
        PART_DiagramControl.Diagram.Grid.Visible = !PART_DiagramControl.Diagram.Grid.Visible;
        // PART_DiagramControl.Background = 
    }
}