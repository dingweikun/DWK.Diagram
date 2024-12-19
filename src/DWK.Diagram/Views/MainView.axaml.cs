using System.Text;
using Avalonia.Interactivity;

namespace DWK.Diagram.Views;

public partial class MainView : UserControl
{
    private const double PanelMinSpace = 60;
    private double _leftPanelWidth = 300;
    private double _leftPanelLowerHeight = 300;

    public MainView()
    {
        InitializeComponent();
        InitializeLayoutControls();
    }

    private void InitializeLayoutControls()
    {
        PART_PanelExplorer.IsVisible = false;
        PART_PanelComponent.IsVisible = false;

        PART_LeftUpperNavListBox.SelectionChanged += OnLeftNavListBoxSelectionChanged;
        PART_LeftLowerNavListBox.SelectionChanged += OnLeftNavListBoxSelectionChanged;

        PART_LeftUpperNavListBox.SelectedItem = null;
        PART_LeftLowerNavListBox.SelectedItem = null;

        PART_LeftGridSplitter.IsVisible = false;
        PART_LeftVerticalGridSplitter.IsVisible = false;

        var leftCol = PART_MainLayoutGrid.ColumnDefinitions[0];
        leftCol.MinWidth = 0;
        leftCol.Width = new GridLength(0, GridUnitType.Pixel);
    }

    private void OnLeftNavListBoxSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not ListBox) return;

        if (e.AddedItems.Count == 0 && e.RemovedItems.Count > 0)
        {
            ResetLeftPanelLayout(isOpening: false);
        }
        else if (e.AddedItems.Count > 0 && e.RemovedItems.Count == 0)
        {
            ResetLeftPanelLayout(isOpening: true);
        }
    }

    private void ResetLeftPanelLayout(bool isOpening)
    {
        if (isOpening)
            AnyLeftPanelOpening();
        else
            AnyLeftPanelClosing();
        return;

        #region local methods

        void AnyLeftPanelOpening()
        {
            var upperOpened = PART_LeftUpperNavListBox.SelectedItem is not null;
            var lowerOpened = PART_LeftLowerNavListBox.SelectedItem is not null;

            if (upperOpened && lowerOpened)
            {
                var actualHeight = PART_LeftLayoutGrid.RowDefinitions[0].ActualHeight + PART_LeftLayoutGrid.RowDefinitions[2].ActualHeight;
                _leftPanelLowerHeight = Double.Min(actualHeight - PanelMinSpace, _leftPanelLowerHeight);

                PART_LeftLayoutGrid.RowDefinitions[0].MinHeight = PanelMinSpace;
                PART_LeftLayoutGrid.RowDefinitions[2].MinHeight = PanelMinSpace;

                PART_LeftLayoutGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                PART_LeftLayoutGrid.RowDefinitions[2].Height = new GridLength(_leftPanelLowerHeight, GridUnitType.Pixel);

                PART_LeftVerticalGridSplitter.IsVisible = true;
            }

            if (upperOpened && !lowerOpened)
            {
                PART_LeftLayoutGrid.RowDefinitions[0].MinHeight = PanelMinSpace;
                PART_LeftLayoutGrid.RowDefinitions[2].MinHeight = 0;

                PART_LeftLayoutGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                PART_LeftLayoutGrid.RowDefinitions[2].Height = new GridLength(0, GridUnitType.Pixel);

                // PART_LeftVerticalGridSplitter.IsVisible = false;
            }

            if (!upperOpened && lowerOpened)
            {
                PART_LeftLayoutGrid.RowDefinitions[0].MinHeight = 0;
                PART_LeftLayoutGrid.RowDefinitions[2].MinHeight = PanelMinSpace;

                PART_LeftLayoutGrid.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Pixel);
                PART_LeftLayoutGrid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                // PART_LeftVerticalGridSplitter.IsVisible = false;
            }

            if (!upperOpened && !lowerOpened)
            {
                throw new InvalidOperationException();
            }

            if (upperOpened != lowerOpened)
            {
                PART_LeftGridSplitter.IsVisible = true;
                PART_MainLayoutGrid.ColumnDefinitions[0].MinWidth = PanelMinSpace;
                PART_MainLayoutGrid.ColumnDefinitions[0].Width = new GridLength(_leftPanelWidth, GridUnitType.Pixel);
            }
        }

        void AnyLeftPanelClosing()
        {
            var upperOpened = PART_LeftUpperNavListBox.SelectedItem is not null;
            var lowerOpened = PART_LeftLowerNavListBox.SelectedItem is not null;

            if (upperOpened && lowerOpened)
            {
                throw new InvalidOperationException();
            }

            if (upperOpened && !lowerOpened)
            {
                _leftPanelLowerHeight = PART_LeftLayoutGrid.RowDefinitions[2].ActualHeight;
                PART_LeftVerticalGridSplitter.IsVisible = false;

                PART_LeftLayoutGrid.RowDefinitions[0].MinHeight = PanelMinSpace;
                PART_LeftLayoutGrid.RowDefinitions[2].MinHeight = 0;

                PART_LeftLayoutGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                PART_LeftLayoutGrid.RowDefinitions[2].Height = new GridLength(0, GridUnitType.Pixel);
            }

            if (!upperOpened && lowerOpened)
            {
                _leftPanelLowerHeight = PART_LeftLayoutGrid.RowDefinitions[2].ActualHeight;
                PART_LeftVerticalGridSplitter.IsVisible = false;

                PART_LeftLayoutGrid.RowDefinitions[0].MinHeight = 0;
                PART_LeftLayoutGrid.RowDefinitions[2].MinHeight = PanelMinSpace;

                PART_LeftLayoutGrid.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Pixel);
                PART_LeftLayoutGrid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
            }

            if (!upperOpened && !lowerOpened)
            {
                // PART_LeftLayoutGrid do nothing

                _leftPanelWidth = PART_MainLayoutGrid.ColumnDefinitions[0].ActualWidth;
                PART_LeftGridSplitter.IsVisible = false;

                PART_MainLayoutGrid.ColumnDefinitions[0].MinWidth = 0;
                PART_MainLayoutGrid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Pixel);
            }
        }

        #endregion
    }
}