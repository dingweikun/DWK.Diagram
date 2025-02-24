using System.Collections.Generic;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace DWK.Diagram.Services;

public class OpenningPageMessage(IDiagramPage value) : ValueChangedMessage<IDiagramPage>(value);

public interface IDiagramDocService
{
    IDiagramDoc? ActiveDoc { get; }

    IReadOnlyList<IDiagramPage> PageList { get; }

    void OpenDoc();

    void OpenPage(IDiagramPage diagramPage);
}

internal partial class DiagramDocService : ObservableRecipient, IDiagramDocService
{
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(PageList))]
    private IDiagramDoc? _activeDoc;

    public IReadOnlyList<IDiagramPage> PageList => ActiveDoc?.Pages ?? [];


    public void OpenPage(IDiagramPage diagramPage)
    {
        ArgumentNullException.ThrowIfNull(diagramPage);
        
        if (ActiveDoc == null)
        {
            throw new InvalidOperationException("No active document");
        }

        if (!ActiveDoc.Pages.Contains(diagramPage))
        {
            throw new ArgumentException("Page does not belong to current document");
        }
        
        Debug.WriteLine($"打开页面 {diagramPage.PageName}");
        Messenger.Send(new OpenningPageMessage(diagramPage));
    }

    // TODO: 测试电气系统
    public void OpenDoc()
    {
        if (ActiveDoc != null) throw new InvalidOperationException("ActiveDoc is not null");

        ActiveDoc = new DiagramDoc<ElecDiagramPage>(Guid.NewGuid(), "测试项目", "这是测试项目")
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