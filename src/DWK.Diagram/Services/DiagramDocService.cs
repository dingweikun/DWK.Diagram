using System.Collections.Generic;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace DWK.Diagram.Services;

public class SetEditingPageMessage(IDiagramPage value) : ValueChangedMessage<IDiagramPage>(value);

public interface IDiagramDocService
{
    IDiagramDoc? CurrentDoc { get; }

    IReadOnlyList<IDiagramPage> PageList { get; }

    void SetEditingPage(IDiagramPage diagramPage);

    void OpenProject();
}

internal partial class DiagramDocService : ObservableRecipient, IDiagramDocService
{
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(PageList))]
    private IDiagramDoc? _currentDoc;

    public IReadOnlyList<IDiagramPage> PageList => CurrentDoc is null ? [] : CurrentDoc.Pages;


    public void SetEditingPage(IDiagramPage diagramPage)
    {
        Console.Error.WriteLine("设置当前编辑页面");
        Messenger.Send(new SetEditingPageMessage(diagramPage));
    }

    // TODO: 测试电气系统
    public void OpenProject()
    {
        CurrentDoc = new DiagramDoc<ElecDiagramPage>(Guid.NewGuid(), "测试项目", "这是测试项目")
        {
            Pages =
            {
                new ElecDiagramPage(Guid.NewGuid(), "测试页面"),
                new ElecDiagramPage(Guid.NewGuid(), "测试页面2"),
                new ElecDiagramPage(Guid.NewGuid(), "测试页面3"),
            }
        };
    }
}