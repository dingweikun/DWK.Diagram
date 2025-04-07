using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Metadata;
using CommunityToolkit.Mvvm.Input;

namespace DWK.Controls;

public class LayoutControl : TemplatedControl
{
    public LayoutControl()
    {
        SlotItems.CollectionChanged += (_, _) => UpdateSlotLayout();

        SlotItem.LayoutInstance = this;

        MoveToLeftTopCommand = new RelayCommand<SlotItem>(item => SetSlotItemPosition(item, SlotPosition.LeftTop));
        MoveToLeftBottomCommand = new RelayCommand<SlotItem>(item => SetSlotItemPosition(item, SlotPosition.LeftBottom));
        MoveToRightTopCommand = new RelayCommand<SlotItem>(item => SetSlotItemPosition(item, SlotPosition.RightTop));
        MoveToRightBottomCommand = new RelayCommand<SlotItem>(item => SetSlotItemPosition(item, SlotPosition.RightBottom));
        MoveToBottomLeftCommand = new RelayCommand<SlotItem>(item => SetSlotItemPosition(item, SlotPosition.BottomLeft));
        MoveToBottomRightCommand = new RelayCommand<SlotItem>(item => SetSlotItemPosition(item, SlotPosition.BottomRight));
        HideCommand = new RelayCommand<SlotItem>(item => DisplaySwitch(item, true));
        ShowCommand = new RelayCommand<SlotItem>(item => DisplaySwitch(item, false));
    }

    public ObservableCollection<SlotItem> SlotItems { get; } = [];

    public void UpdateSlotLayout()
    {
        List<SlotItem> hidden = [], lt = [], lb = [], rt = [], rb = [], bl = [], br = [];

        var s_lt = SlotLeftTopSelectedItem;
        var s_lb = SlotLeftBottomSelectedItem;
        var s_rt = SlotRightTopSelectedItem;
        var s_rb = SlotRightBottomSelectedItem;
        var s_bl = SlotBottomLeftSelectedItem;
        var s_br = SlotBottomRightSelectedItem;

        foreach (var item in SlotItems)
        {
            if (item.IsHidden) hidden.Add(item);
            else
            {
                switch (item.Position)
                {
                    case SlotPosition.LeftTop:
                        lt.Add(item);
                        break;
                    case SlotPosition.LeftBottom:
                        lb.Add(item);
                        break;
                    case SlotPosition.RightTop:
                        rt.Add(item);
                        break;
                    case SlotPosition.RightBottom:
                        rb.Add(item);
                        break;
                    case SlotPosition.BottomLeft:
                        bl.Add(item);
                        break;
                    case SlotPosition.BottomRight:
                        br.Add(item);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        HiddenSlotItems = hidden;
        SlotLeftTopChildren = lt;
        SlotLeftBottomChildren = lb;
        SlotRightTopChildren = rt;
        SlotRightBottomChildren = rb;
        SlotBottomLeftChildren = bl;
        SlotBottomRightChildren = br;

        SlotLeftTopSelectedItem = s_lt;
        SlotLeftBottomSelectedItem = s_lb;
        SlotRightTopSelectedItem = s_rt;
        SlotRightBottomSelectedItem = s_rb;
        SlotBottomLeftSelectedItem = s_bl;
        SlotBottomRightSelectedItem = s_br;
    }

    private void DisplaySwitch(SlotItem? slotItem, bool hidden)
    {
        if (slotItem is null || slotItem.IsHidden == hidden) return;

        slotItem.IsHidden = hidden;
        UpdateSlotLayout();

        if (!slotItem.IsHidden)
            SetSlotSelectedItem(slotItem.Position, slotItem);
    }


    // 定义 HidedSlotItems 的相关属性
    public static readonly StyledProperty<IList<SlotItem>> HiddenSlotItemsProperty =
        AvaloniaProperty.Register<LayoutControl, IList<SlotItem>>(nameof(HiddenSlotItems), []);

    private IList<SlotItem> HiddenSlotItems
    {
        get => GetValue(HiddenSlotItemsProperty);
        set => SetValue(HiddenSlotItemsProperty, value);
    }

    // 定义 SlotLeftTop 的相关属性
    public static readonly StyledProperty<SlotItem?> SlotLeftTopSelectedItemProperty =
        AvaloniaProperty.Register<LayoutControl, SlotItem?>(nameof(SlotLeftTopSelectedItem), null,
            coerce: (avaloniaObject, control) => avaloniaObject is LayoutControl layoutControl
                ? layoutControl.SlotLeftTopChildren.SingleOrDefault(c => c == control, null)
                : null);

    public static readonly StyledProperty<IList<SlotItem>> SlotLeftTopChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, IList<SlotItem>>(nameof(SlotLeftTopChildren), []);

    private SlotItem? SlotLeftTopSelectedItem
    {
        get => GetValue(SlotLeftTopSelectedItemProperty);
        set => SetValue(SlotLeftTopSelectedItemProperty, value);
    }

    private IList<SlotItem> SlotLeftTopChildren
    {
        get => GetValue(SlotLeftTopChildrenProperty);
        set => SetValue(SlotLeftTopChildrenProperty, value);
    }

    // 定义 SlotLeftBottom 的相关属性
    public static readonly StyledProperty<SlotItem?> SlotLeftBottomSelectedItemProperty =
        AvaloniaProperty.Register<LayoutControl, SlotItem?>(nameof(SlotLeftBottomSelectedItem), null,
            coerce: (avaloniaObject, control) => avaloniaObject is LayoutControl layoutControl
                ? layoutControl.SlotLeftBottomChildren.SingleOrDefault(c => c == control, null)
                : null);

    public static readonly StyledProperty<IList<SlotItem>> SlotLeftBottomChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, IList<SlotItem>>(nameof(SlotLeftBottomChildren), []);

    private SlotItem? SlotLeftBottomSelectedItem
    {
        get => GetValue(SlotLeftBottomSelectedItemProperty);
        set => SetValue(SlotLeftBottomSelectedItemProperty, value);
    }

    private IList<SlotItem> SlotLeftBottomChildren
    {
        get => GetValue(SlotLeftBottomChildrenProperty);
        set => SetValue(SlotLeftBottomChildrenProperty, value);
    }

    // 定义 SlotRightTop 的相关属性
    public static readonly StyledProperty<SlotItem?> SlotRightTopSelectedItemProperty =
        AvaloniaProperty.Register<LayoutControl, SlotItem?>(nameof(SlotRightTopSelectedItem), null,
            coerce: (avaloniaObject, control) => avaloniaObject is LayoutControl layoutControl
                ? layoutControl.SlotRightTopChildren.SingleOrDefault(c => c == control, null)
                : null);

    public static readonly StyledProperty<IList<SlotItem>> SlotRightTopChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, IList<SlotItem>>(nameof(SlotRightTopChildren), []);

    private SlotItem? SlotRightTopSelectedItem
    {
        get => GetValue(SlotRightTopSelectedItemProperty);
        set => SetValue(SlotRightTopSelectedItemProperty, value);
    }

    private IList<SlotItem> SlotRightTopChildren
    {
        get => GetValue(SlotRightTopChildrenProperty);
        set => SetValue(SlotRightTopChildrenProperty, value);
    }

    // 定义 SlotRightBottom 的相关属性
    public static readonly StyledProperty<SlotItem?> SlotRightBottomSelectedItemProperty =
        AvaloniaProperty.Register<LayoutControl, SlotItem?>(nameof(SlotRightBottomSelectedItem), null,
            coerce: (avaloniaObject, control) => avaloniaObject is LayoutControl layoutControl
                ? layoutControl.SlotRightBottomChildren.SingleOrDefault(c => c == control, null)
                : null);

    public static readonly StyledProperty<IList<SlotItem>> SlotRightBottomChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, IList<SlotItem>>(nameof(SlotRightBottomChildren), []);

    private SlotItem? SlotRightBottomSelectedItem
    {
        get => GetValue(SlotRightBottomSelectedItemProperty);
        set => SetValue(SlotRightBottomSelectedItemProperty, value);
    }

    private IList<SlotItem> SlotRightBottomChildren
    {
        get => GetValue(SlotRightBottomChildrenProperty);
        set => SetValue(SlotRightBottomChildrenProperty, value);
    }

    // 定义 SlotBottomLeft 的相关属性
    public static readonly StyledProperty<SlotItem?> SlotBottomLeftSelectedItemProperty =
        AvaloniaProperty.Register<LayoutControl, SlotItem?>(nameof(SlotBottomLeftSelectedItem), null,
            coerce: (avaloniaObject, control) => avaloniaObject is LayoutControl layoutControl
                ? layoutControl.SlotBottomLeftChildren.SingleOrDefault(c => c == control, null)
                : null);

    public static readonly StyledProperty<IList<SlotItem>> SlotBottomLeftChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, IList<SlotItem>>(nameof(SlotBottomLeftChildren), []);

    private SlotItem? SlotBottomLeftSelectedItem
    {
        get => GetValue(SlotBottomLeftSelectedItemProperty);
        set => SetValue(SlotBottomLeftSelectedItemProperty, value);
    }

    private IList<SlotItem> SlotBottomLeftChildren
    {
        get => GetValue(SlotBottomLeftChildrenProperty);
        set => SetValue(SlotBottomLeftChildrenProperty, value);
    }

    // 定义 SlotBottomRight 的相关属性
    public static readonly StyledProperty<SlotItem?> SlotBottomRightSelectedItemProperty =
        AvaloniaProperty.Register<LayoutControl, SlotItem?>(nameof(SlotBottomRightSelectedItem), null,
            coerce: (avaloniaObject, control) => avaloniaObject is LayoutControl layoutControl
                ? layoutControl.SlotBottomRightChildren.SingleOrDefault(c => c == control, null)
                : null);

    public static readonly StyledProperty<IList<SlotItem>> SlotBottomRightChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, IList<SlotItem>>(nameof(SlotBottomRightChildren), []);

    private SlotItem? SlotBottomRightSelectedItem
    {
        get => GetValue(SlotBottomRightSelectedItemProperty);
        set => SetValue(SlotBottomRightSelectedItemProperty, value);
    }

    private IList<SlotItem> SlotBottomRightChildren
    {
        get => GetValue(SlotBottomRightChildrenProperty);
        set => SetValue(SlotBottomRightChildrenProperty, value);
    }


    // 新增 Content 属性
    public static readonly StyledProperty<Control?> ContentProperty =
        AvaloniaProperty.Register<LayoutControl, Control?>(nameof(Content), null);

    public Control? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    #region ContextMenu Commands

    public ICommand HideCommand { get; }
    public ICommand ShowCommand { get; }
    public ICommand MoveToLeftTopCommand { get; }
    public ICommand MoveToLeftBottomCommand { get; }
    public ICommand MoveToRightTopCommand { get; }
    public ICommand MoveToRightBottomCommand { get; }
    public ICommand MoveToBottomLeftCommand { get; }
    public ICommand MoveToBottomRightCommand { get; }

    private void SetSlotItemPosition(SlotItem? slotItem, SlotPosition position)
    {
        if (slotItem is null || slotItem.Position == position) return;

        var isSelected = slotItem == slotItem.Position switch
        {
            SlotPosition.LeftTop => SlotLeftTopSelectedItem,
            SlotPosition.LeftBottom => SlotLeftBottomSelectedItem,
            SlotPosition.RightTop => SlotRightTopSelectedItem,
            SlotPosition.RightBottom => SlotRightBottomSelectedItem,
            SlotPosition.BottomLeft => SlotBottomLeftSelectedItem,
            SlotPosition.BottomRight => SlotBottomRightSelectedItem,
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };

        slotItem.Position = position;
        UpdateSlotLayout();

        if (isSelected)
            SetSlotSelectedItem(position, slotItem);
    }

    private void SetSlotSelectedItem(SlotPosition position, SlotItem? slotItem)
    {
        switch (position)
        {
            case SlotPosition.LeftTop:
                SlotLeftTopSelectedItem = slotItem;
                break;
            case SlotPosition.LeftBottom:
                SlotLeftBottomSelectedItem = slotItem;
                break;
            case SlotPosition.RightTop:
                SlotRightTopSelectedItem = slotItem;
                break;
            case SlotPosition.RightBottom:
                SlotRightBottomSelectedItem = slotItem;
                break;
            case SlotPosition.BottomLeft:
                SlotBottomLeftSelectedItem = slotItem;
                break;
            case SlotPosition.BottomRight:
                SlotBottomRightSelectedItem = slotItem;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(position), position, null);
        }
    }

    #endregion
}

public enum SlotPosition
{
    LeftTop,
    LeftBottom,
    RightTop,
    RightBottom,
    BottomLeft,
    BottomRight
}

public static class SlotPositionConverters
{
    public static IsConverter Is { get; } = new IsConverter();
    public static NotConverter Not { get; } = new NotConverter();

    public class IsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            value is SlotPosition pos1 && parameter is SlotPosition pos2 && pos1 == pos2;

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class NotConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            value is SlotPosition pos1 && parameter is SlotPosition pos2 && pos1 != pos2;

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}

public class SlotItem
{
    public static LayoutControl? LayoutInstance { get; internal set; }

    public SlotPosition Position { get; set; }

    public bool IsHidden { get; set; }

    [Content] public required Control Content { get; init; }

    public required string Title { get; init; }

    public Geometry? IconPath { get; init; } = Geometry.Parse("M 40,20 L 60,40 L 40,60 L 20,40 Z");

    public double IconHeight { get; init; } = 20.0;

    public double IconWidth { get; init; } = 20.0;
}

public static class SlotItemConverters
{
    public static SlotItemContentConverter Content { get; } = new();

    public class SlotItemContentConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is SlotItem item ? item.Content : null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public static IsConverter Is { get; } = new IsConverter();
    public static NotConverter Not { get; } = new NotConverter();

    public class IsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            value is SlotItem item && parameter is SlotPosition pos && item.Position == pos;

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class NotConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            value is SlotItem item && parameter is SlotPosition pos && item.Position != pos;

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}

public static class IListConverters
{
    public static IsEmptyConverter IsEmpty { get; } = new();
    public static IsNotEmptyConverter IsNotEmpty { get; } = new();

    public class IsEmptyConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is IList { Count: 0 };
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsNotEmptyConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is IList { Count: > 0 };
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

public static class DebugConverters
{
    public static TypeConverter Type { get; } = new();

    public class TypeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value?.GetType().Name ?? "Null";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}