namespace DWK.Diagram.ViewModels;

public partial class DiagramPageTabViewModel : ViewModelBase, IRecipient<SetEditingPageMessage>
{
    public ObservableCollection<IDiagramControl> DiagramControls { get; } = [];

    [ObservableProperty] private IDiagramControl? _currentPageDiagram;
    
    public DiagramPageTabViewModel()
    {
        IsActive = true; // 激活 IRecipient 消息
    }
    
   
    // NOTE: 在此处根据页面数据类型，创建相应的 IDiagramControl 对象，实现统一建模
    protected IDiagramControl CreateDiagramPageControl(IDiagramPage pageData)
    {
        switch (pageData)
        {
            case ElecDiagramPage elecDiagramPage:
                return new ElecDiagramControl(elecDiagramPage);
            default:
                return null;
        }
    }


    public void Receive(SetEditingPageMessage message)
    {
        Console.WriteLine("收到消息");

        var control = DiagramControls.SingleOrDefault(ctrl => ctrl.DiagramPage == message.Value);
        if (control is null)
        {
            control = CreateDiagramPageControl(message.Value);
            DiagramControls.Add(control);
        }

        CurrentPageDiagram = control;
    }

    [RelayCommand]
    private void ClosePage(IDiagramPage diagramPage)
    {
        var control = DiagramControls.SingleOrDefault(ctrl => ctrl.DiagramPage == diagramPage);
        if (control is not null) DiagramControls.Remove(control);
    }

    [RelayCommand]
    private void CloseAllPages()
    {
        DiagramControls.Clear();
    }
}