using System.Collections.ObjectModel;
using Avalonia.Animation;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DWK.Diagram.Controls;
using DWK.Diagram.Models;
using DWK.Diagram.Services;

namespace DWK.Diagram.ViewModels;

public partial class PageViewModel : ViewModelBase, IRecipient<SetEditingPageMessage>
{
    public ObservableCollection<DiagramPage> OpenedPages { get; } = [];

    public ObservableCollection<TestControl> OpenedControl { get; } = [];

    [ObservableProperty] private DiagramPage? _currentPage;
    [ObservableProperty] private TestControl? _currentControl;

    public PageViewModel()
    {
        this.IsActive = true;
    }


    public void Receive(SetEditingPageMessage message)
    {
        Console.WriteLine("收到消息");
        
        if (OpenedPages.Contains(message.Value) is false)
            OpenedPages.Add(message.Value);
        CurrentPage = message.Value;

        if (OpenedControl.Any(ctrl => ctrl.DiagramPage == message.Value) is false)
            OpenedControl.Add(new TestControl { DiagramPage = message.Value });
    }

    [RelayCommand]
    private void ClosePage(DiagramPage page)
    {
        OpenedPages.Remove(page);
    }

    [RelayCommand]
    private void CloseAllPages()
    {
        OpenedPages.Clear();
    }
}