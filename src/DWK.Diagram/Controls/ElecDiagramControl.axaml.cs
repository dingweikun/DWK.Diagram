using Avalonia.Interactivity;
using Avalonia;
using DWK.Diagram.Node;

namespace DWK.Diagram.Controls;

public partial class ElecDiagramControl : UserControl, IDiagramControl
{
    #region GridVisibleProperty

    public static readonly DirectProperty<ElecDiagramControl, bool> GridVisibleProperty =
        AvaloniaProperty.RegisterDirect<ElecDiagramControl, bool>(
            nameof(GridVisible), o => o.GridVisible, (o, v) => o.GridVisible = v);

    private bool _gridVisible;

    public bool GridVisible
    {
        get => _gridVisible;
        set => SetAndRaise(GridVisibleProperty, ref _gridVisible, value);
    }

    #endregion

    #region IsLockedProperty

    public static readonly DirectProperty<ElecDiagramControl, bool> IsLockedProperty =
        AvaloniaProperty.RegisterDirect<ElecDiagramControl, bool>(
            nameof(IsLocked), o => o.IsLocked, (o, v) => o.IsLocked = v);

    private bool _isLocked;

    public bool IsLocked
    {
        get => _isLocked;
        set => SetAndRaise(IsLockedProperty, ref _isLocked, value);
    }

    #endregion

    private readonly ElecDiagramPage _diagramPage;

    public IDiagramPage DiagramPage => _diagramPage;

    public ElecDiagramControl(ElecDiagramPage diagramPage)
    {
        _diagramPage = diagramPage;

        InitializeComponent();
        InitDiagramControl();

        GridVisible = PART_GoDiagramControl.Diagram.Grid.Visible;
        IsLocked = PART_GoDiagramControl.Diagram.IsReadOnly;
    }

    private void InitDiagramControl()
    {
        var builder = new ElecDiagramBuilder();
        builder.BuildDiagram(PART_GoDiagramControl.Diagram);

        PART_GoDiagramControl.Diagram.ToolManager.DraggingTool.IsGridSnapEnabled = true;
        PART_GoDiagramControl.Diagram.ToolManager.ResizingTool.IsGridSnapEnabled = true;
    }


    private void GridVisibleButton_OnClick(object? sender, RoutedEventArgs e)
    {
        GridVisible = PART_GoDiagramControl.Diagram.Grid.Visible = !PART_GoDiagramControl.Diagram.Grid.Visible;
    }

    private void IsLockedButton_OnClick(object? sender, RoutedEventArgs e)
    {
        IsLocked = PART_GoDiagramControl.Diagram.IsReadOnly = !PART_GoDiagramControl.Diagram.IsReadOnly;
    }
}