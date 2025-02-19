using System.Diagnostics;

namespace DWK.Diagram.Controls;

public interface IDiagramControl
{
    public IDiagramPage DiagramPage { get; }
}

public static class DiagramControlFactory
{
    public static IDiagramControl? CreateDiagramControl(IDiagramPage diagramPage)
    {
        if (diagramPage is ElecDiagramPage page)
        {
            Debug.WriteLine("创建电气建模页面控件");
            return new ElecDiagramControl(page);
        }
        else
        {
            return null;
        }
    }
}