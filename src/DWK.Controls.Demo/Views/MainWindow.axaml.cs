using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace DWK.Controls.Demo.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void SelectSlot1Item1(object sender, RoutedEventArgs e)
    {
        var slot1Item1 = MyLayoutControl.Slot1Children.FirstOrDefault(tb => (tb as TextBlock)?.Text == "Slot 1 Item 1");
        MyLayoutControl.SlotLeftSelectedItem = slot1Item1;
    }

    private void SelectSlot2Item1(object sender, RoutedEventArgs e)
    {
        var slot2Item1 = MyLayoutControl.Slot2Children.FirstOrDefault(tb => (tb as TextBlock)?.Text == "Slot 2 Item 1");
        MyLayoutControl.SlotRightSelectedItem = slot2Item1;
    }

    private void SelectSlot1WithSlot2Item1(object? sender, RoutedEventArgs e)
    {
        var slot2Item1 = MyLayoutControl.Slot2Children.FirstOrDefault(tb => (tb as TextBlock)?.Text == "Slot 2 Item 1");
        MyLayoutControl.SlotLeftSelectedItem = slot2Item1;
    }

    private void SelectSlot2WithSlot1Item1(object? sender, RoutedEventArgs e)
    {
        var slot1Item1 = MyLayoutControl.Slot1Children.FirstOrDefault(tb => (tb as TextBlock)?.Text == "Slot 1 Item 1");
        MyLayoutControl.SlotRightSelectedItem = slot1Item1;
    }
}