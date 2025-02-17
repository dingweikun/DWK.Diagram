using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DWK.Diagram.Models;

namespace DWK.Diagram.Controls;

public partial class TestControl : UserControl
{
    public string Id { get; } = Guid.NewGuid().ToString();

    #region DiagramPageProperty

    public static readonly DirectProperty<TestControl, DiagramPage?> DiagramPageProperty =
        AvaloniaProperty.RegisterDirect<TestControl, DiagramPage?>(
            nameof(DiagramPage),
            o => o.DiagramPage,
            (o, v) => o.DiagramPage = v);

    private DiagramPage? _diagramPage;

    public DiagramPage? DiagramPage
    {
        get => _diagramPage;
        set => SetAndRaise(DiagramPageProperty, ref _diagramPage, value);
    }

    #endregion

    public TestControl()
    {
        InitializeComponent();
        

        var diagram = PART_DiagramControl.Diagram;
        diagram.AnimationManager.IsEnabled = false;
        diagram.Add((new Go.Node("Auto")
            .Add(
                new Go.TextBlock()
                {
                    Text = Id,
                    Font = new Go.Font("Noto Sans CJK SC", 20)
                }
            )));
    }
}