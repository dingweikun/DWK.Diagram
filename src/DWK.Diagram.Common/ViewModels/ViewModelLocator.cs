using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace DWK.Diagram.ViewModels;

public static class ViewModelLocator
{
    private const string ViewEndString = "View";
    private const string ViewModelEndString = "ViewModel";
    private const string ViewSpaceString = "Views";
    private const string ViewModelSpaceString = "ViewModels";

    public static object GetViewModel(UserControl view)
    {
        var vType = view.GetType();
        var vName = vType.Name;
        var vSpace = vType.Namespace!;
        if (!vName.EndsWith(ViewEndString) || !vSpace.EndsWith(ViewSpaceString))
            throw new InvalidOperationException();

        var vmName = vName[..^ViewEndString.Length] + ViewModelEndString;
        var vmSpace = vSpace[..^ViewSpaceString.Length] + ViewModelSpaceString;
        var type = vType.Assembly.GetType($"{vmSpace}.{vmName}") ?? throw new InvalidOperationException();

        var app = Avalonia.Application.Current as IServiceProviderApp ?? throw new InvalidOperationException();
        var vm = app.Services.GetRequiredService(type);

        if (vm is ObservableRecipient recipient)
            recipient.IsActive = true;

        return vm;
    }
}