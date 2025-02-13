using System.Collections.ObjectModel;
using Avalonia.Animation;
using CommunityToolkit.Mvvm.Messaging;
using DWK.Diagram.Models;
using DWK.Diagram.Services;

namespace DWK.Diagram.ViewModels;

public partial class PageViewModel : ViewModelBase, IRecipient<SetEditingPageMessage>
{
    public ObservableCollection<DiagramPage> OpenedPages { get; } = [];

    [ObservableProperty] private DiagramPage? _currentPage;

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
    }
}