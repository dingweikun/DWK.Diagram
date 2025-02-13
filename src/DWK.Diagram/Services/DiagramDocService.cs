using System.Collections.Generic;
using System.Collections.ObjectModel;
using DWK.Diagram.Models;

namespace DWK.Diagram.Services;

public interface IDiagramDocService
{
    DiagramDoc? CurrentDoc { get; }
    string CurrentDocPath { get; }
    IReadOnlyList<DiagramPage> PageList { get; }
    ObservableCollection<DiagramPage> OpenedPages { get; }

    void CloseProject();
    void ClosePage(DiagramPage page);
    void SavePage(DiagramPage page);
    void OpenPage(Guid pageId);
    void OpenProject(string filePath);
}

internal partial class DiagramDocService : ObservableObject, IDiagramDocService
{
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(PageList))]
    private DiagramDoc? _currentDoc;

    [ObservableProperty] private string _currentDocPath = string.Empty;

    [ObservableProperty] private ObservableCollection<DiagramPage> _openedPages = [];

    public IReadOnlyList<DiagramPage> PageList => CurrentDoc is null ? [] : CurrentDoc.Pages;


    public void CloseProject()
    {
        OpenedPages.Clear();
        CurrentDoc = null;
        CurrentDocPath = string.Empty;
    }

    public void ClosePage(DiagramPage page)
    {
        OpenedPages.Remove(page);
    }

    public void SavePage(DiagramPage page)
    {
        if (CurrentDoc is null) throw new InvalidOperationException();

        var index = CurrentDoc.Pages.FindIndex(pg => pg.Id == page.Id);
        if (index != -1)
            CurrentDoc.Pages[index] = page;
        else
            CurrentDoc.Pages.Add(page);

        // TODO: 保存到本地文件 -----
    }

    public void OpenPage(Guid pageId)
    {
        if (OpenedPages.Any(pg => pg.Id == pageId)) return;

        var page = CurrentDoc.Pages.SingleOrDefault(pg => pg.Id == pageId);
        if (page is not null)
        {
            OpenedPages.Add(page);
        }
    }

    public void OpenProject(string filePath)
    {
        // TODO: 代开本地文件

        CurrentDoc = DiagramDoc.TestInstance;
        CurrentDocPath = filePath;

        OpenedPages.Clear();
    }
}