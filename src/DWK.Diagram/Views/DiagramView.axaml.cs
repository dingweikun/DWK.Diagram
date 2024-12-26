using System.Collections.Generic;

namespace DWK.Diagram.Views;

public partial class DiagramView : UserControl
{
    public DiagramView()
    {
        InitializeComponent();

        var builder = new LogicDiagramBuilder()
            .WithSettings(settings =>
            {
                settings.ModuleBackColor = "LightYellow";

            });
        
        
        builder.BuildDiagram(PART_DiagramControl.Diagram);
        
      

    }

   


}