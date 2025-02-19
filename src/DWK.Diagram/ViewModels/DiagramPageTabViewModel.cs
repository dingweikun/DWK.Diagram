using System.Collections.Generic;

namespace DWK.Diagram.ViewModels;

public partial class DiagramPageTabViewModel : ViewModelBase, IRecipient<OpenningPageMessage>
{
    public ObservableCollection<IDiagramPage> OpenedPages { get; } = [];

    [ObservableProperty, NotifyPropertyChangedFor(nameof(CurrentDiagram))]
    private IDiagramPage? _currentPage;

    public IDiagramControl? CurrentDiagram
    {
        get
        {
            if (CurrentPage is null) return null;

            if (DiagramControlMap.TryGetValue(CurrentPage.Id, out var diagram))
                return diagram;

            var newDiagram = DiagramControlFactory.CreateDiagramControl(CurrentPage);
            if (newDiagram is not null) DiagramControlMap.Add(CurrentPage.Id, newDiagram);
            return newDiagram;
        }
    }


    private Dictionary<Guid, IDiagramControl> DiagramControlMap { get; } = [];


    public DiagramPageTabViewModel()
    {
        IsActive = true; // 激活 IRecipient 消息
    }


    public void Receive(OpenningPageMessage message)
    {
        Console.WriteLine($"收到消息 {nameof(OpenningPageMessage)}");

        if (OpenedPages.Contains(message.Value) is false)
            OpenedPages.Add(message.Value);

        CurrentPage = message.Value;
    }

    [RelayCommand]
    private void ClosePage(IDiagramPage diagramPage)
    {
        OpenedPages.Remove(diagramPage);
        DiagramControlMap.Remove(diagramPage.Id);
    }

    [RelayCommand]
    private void CloseAllPages()
    {
        OpenedPages.Clear();
        DiagramControlMap.Clear();
    }
}