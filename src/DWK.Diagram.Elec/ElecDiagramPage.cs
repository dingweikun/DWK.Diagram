using DWK.Diagram.Models;

namespace DWK.Diagram;

public class ElecDiagramPage(Guid id, string pageName) : IDiagramPage
{
    public Guid Id { get; } = id;
    public string PageName { get; set; } = pageName;
}