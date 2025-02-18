namespace DWK.Diagram.Models;

public interface IDiagramPage
{
    Guid Id { get; }
    string PageName { get; }
}

public interface IDiagramDoc
{
    Guid Id { get; }
    string ProjectName { get; set; }
    string Description { get; set; }
    Type PageType { get; }
    List<IDiagramPage> Pages { get; }
}

public class DiagramDoc<TPage>(Guid id, string projectName, string description = "") : IDiagramDoc where TPage : IDiagramPage
{
    public Guid Id { get; } = id;
    public string ProjectName { get; set; } = projectName;
    public string Description { get; set; } = description;
    public Type PageType => typeof(TPage);
    public List<IDiagramPage> Pages { get; init; } = [];
}