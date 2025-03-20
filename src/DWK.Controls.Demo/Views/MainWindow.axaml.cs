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

    // private void SelectSlot1Item1(object sender, RoutedEventArgs e)
    // {
    //     var slot1Item1 = MyLayoutControl.SlotLeftChildren.FirstOrDefault(tb => (tb as TextBlock)?.Text == "Slot 1 Item 1");
    //     MyLayoutControl.SlotLeftSelectedItem = new SlotItem { Title = "Slot 1 Item 1", Content = slot1Item1, Icon = new PathIcon() };
    // }
    //
    // private void SelectSlot2Item1(object sender, RoutedEventArgs e)
    // {
    //     var slot2Item1 = MyLayoutControl.SlotRightChildren.FirstOrDefault(tb => (tb as TextBlock)?.Text == "Slot 2 Item 1");
    //     MyLayoutControl.SlotRightSelectedItem = new SlotItem { Title = "Slot 2 Item 1", Content = slot2Item1, Icon = new PathIcon() };
    // }
    //
    // private void SelectSlot1WithSlot2Item1(object? sender, RoutedEventArgs e)
    // {
    //     var slot2Item1 = MyLayoutControl.SlotRightChildren.FirstOrDefault(tb => (tb as TextBlock)?.Text == "Slot 2 Item 1");
    //     MyLayoutControl.SlotLeftSelectedItem = new SlotItem { Title = "Slot 2 Item 1", Content = slot2Item1, Icon = new PathIcon() };
    // }
    //
    // private void SelectSlot2WithSlot1Item1(object? sender, RoutedEventArgs e)
    // {
    //     var slot1Item1 = MyLayoutControl.SlotLeftChildren.FirstOrDefault(tb => (tb as TextBlock)?.Text == "Slot 1 Item 1");
    //     MyLayoutControl.SlotRightSelectedItem = new SlotItem { Title = "Slot 1 Item 1", Content = slot1Item1, Icon = new PathIcon() };
    // }
}