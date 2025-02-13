using System.Collections.Generic;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DWK.Diagram.Models;

namespace DWK.Diagram.Services;

public class SetEditingPageMessage(DiagramPage value) : ValueChangedMessage<DiagramPage>(value);

public interface IDiagramDocService
{
    DiagramDoc? CurrentDoc { get; }

    IReadOnlyList<DiagramPage> PageList { get; }

    void SetEditingPage(DiagramPage page);

    void OpenProject();
}

internal partial class DiagramDocService : ObservableRecipient, IDiagramDocService
{
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(PageList))]
    private DiagramDoc? _currentDoc;

    public IReadOnlyList<DiagramPage> PageList => CurrentDoc is null ? [] : CurrentDoc.Pages;

    

    public void SetEditingPage(DiagramPage page)
    {
        Console.Error.WriteLine("设置当前编辑页面");
        Messenger.Send(new SetEditingPageMessage(page));
    }

    public void OpenProject()
    {
        CurrentDoc = DiagramDoc.TestInstance;
    }
}