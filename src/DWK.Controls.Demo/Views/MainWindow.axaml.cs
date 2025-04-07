using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace DWK.Controls.Demo.Views;

public partial class MainWindow : Window
{
    // private readonly SlotItem _item;

    public MainWindow()
    {
        InitializeComponent();

        // _item = new SlotItem()
        // {
        //     Title = "Mov Slot",
        //     Position = SlotPosition.LeftBottom,
        //     IconPath = Geometry.Parse("M 40,20 L 60,40 L 40,60 L 20,40 Z"),
        //     Content = new TextBlock()
        //     {
        //         Text = "Mov Slot",
        //         FontSize = 40,
        //         FontWeight = FontWeight.Bold,
        //         Foreground = new SolidColorBrush(Colors.Red)
        //     }
        // };
        // MyLayoutControl.SlotItems.Add(_item);
    }

    public List<string> SelectionItems { get; set; } = new List<string>
    {
        "选项一",
        "选项二",
        "选项三",
        "选项四"
    };


    // private void Button_OnClick(object? sender, RoutedEventArgs e)
    // {
    //     // _item.Position = _item.Position + 1 > SlotPosition.BottomRight ? SlotPosition.None : _item.Position + 1;
    //     // MyLayoutControl.UpdateSlotLayout();
    // }
    //
    // #region ContextMenu Methods
    //
    // private void TrySetSlotItemPosition(object? sender, SlotPosition position)
    // {
    //     if (sender is not MenuItem { DataContext: SlotItem slotItem } || slotItem.Position == position) return;
    //     slotItem.Position = position;
    //     MyLayoutControl.UpdateSlotLayout();
    // }
    //
    //
    // private void MoveSlotItemToLeftTop(object? sender, RoutedEventArgs e) => TrySetSlotItemPosition(sender, SlotPosition.LeftTop);
    //
    // private void MoveSlotItemToLeftBottom(object? sender, RoutedEventArgs e) => TrySetSlotItemPosition(sender, SlotPosition.LeftBottom);
    //
    // private void MoveSlotItemToRightTop(object? sender, RoutedEventArgs e) => TrySetSlotItemPosition(sender, SlotPosition.RightTop);
    //
    // private void MoveSlotItemToRightBottom(object? sender, RoutedEventArgs e) => TrySetSlotItemPosition(sender, SlotPosition.RightBottom);
    //
    // private void MoveSlotItemToBottomLeft(object? sender, RoutedEventArgs e) => TrySetSlotItemPosition(sender, SlotPosition.BottomLeft);
    //
    // private void MoveSlotItemToBottomRight(object? sender, RoutedEventArgs e) => TrySetSlotItemPosition(sender, SlotPosition.BottomRight);
    //
    // #endregion
     
    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem menuItem)
        {
            string selectedItem = menuItem.Header.ToString();
            // 这里可以添加处理选中项的逻辑，例如显示选中项信息
            System.Console.WriteLine($"你选择了: {selectedItem}");
        }
    }
}