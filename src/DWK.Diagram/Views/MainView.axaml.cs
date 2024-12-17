using Avalonia.Interactivity;

namespace DWK.Diagram.Views;

public partial class MainView : UserControl
{
    private const double LeftPanelMinWidth = 60;
    private double _leftPanelWidth = 300;

    #region LeftPanelOpenedProperty

    public static readonly DirectProperty<MainView, bool> LeftPanelOpenedProperty =
        AvaloniaProperty.RegisterDirect<MainView, bool>(nameof(LeftPanelOpened),
            o => o.LeftPanelOpened,
            (o, v) => o.LeftPanelOpened = v);

    private bool _leftPanelOpened;

    public bool LeftPanelOpened
    {
        get => _leftPanelOpened;
        set => SetAndRaise(LeftPanelOpenedProperty, ref _leftPanelOpened, value);
    }

    #endregion

    public MainView()
    {
        InitializeComponent();
    }

    #region Overried Methods

    protected override void OnLoaded(RoutedEventArgs e)
    {
        SetLeftPanelLayout(LeftPanelOpened);
        base.OnLoaded(e);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        if (change.Property == LeftPanelOpenedProperty)
        {
            SetLeftPanelLayout(LeftPanelOpened);
        }

        base.OnPropertyChanged(change);
    }

    #endregion

    #region Private Methods

    private void SetLeftPanelLayout(bool panelOpened)
    {
        var leftCol = PART_MainLayoutGrid.ColumnDefinitions[0];

        if (panelOpened)
        {
            leftCol.MinWidth = LeftPanelMinWidth;
            leftCol.Width = new GridLength(double.Max(_leftPanelWidth, LeftPanelMinWidth), GridUnitType.Pixel);
        }
        else
        {
            _leftPanelWidth = leftCol.ActualWidth;
            leftCol.MinWidth = 0;
            leftCol.Width = new GridLength(0, GridUnitType.Pixel);
        }

        PART_LeftPanelGridSplitter.IsVisible = panelOpened;
    }

    #endregion
}