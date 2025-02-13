namespace DWK.Diagram.Models;

public class DiagramDoc
{
    public Guid Id { get; }
    public string ProjectName { get; set; }
    public string Description { get; set; }
    public List<DiagramPage> Pages { get; }

    public DiagramDoc(string projectName, string description = "")
    {
        Id = Guid.NewGuid();
        ProjectName = projectName;
        Description = description;
        Pages = new List<DiagramPage>();
    }

    public void AddPage(DiagramPage page)
    {
        Pages.Add(page);
    }

    // 静态方法，返回一个用于测试的 DiagramDoc 实例
    private static DiagramDoc GetTestInstance()
    {
        var doc = new DiagramDoc("Test Project", "This is a test project for DiagramDoc.");
        doc.AddPage(new DiagramPage("Page 1", 800, 600));
        doc.AddPage(new DiagramPage("Page 2", 1024, 768));
        doc.AddPage(new DiagramPage("Page 3", 1280, 1024));
        return doc;
    }

    public static DiagramDoc TestInstance => GetTestInstance();
}

public class DiagramPage
{
    public Guid Id { get; }
    public string PageName { get; set; }
    public (int, int) PageSize { get; set; }

    public DiagramPage(string pageName, int pageHeight, int pageWidth)
    {
        Id = Guid.NewGuid();
        PageName = pageName;
        PageSize = (pageWidth, pageHeight);
    }
}