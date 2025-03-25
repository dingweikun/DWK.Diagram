using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Metadata;

namespace DWK.Controls;

public class LayoutControl : TemplatedControl
{
    public LayoutControl()
    {
        ItemsControl it = new();
        SelectingItemsControl it2 = new();

        SlotItems.CollectionChanged += (_, _) => UpdateSlotLayout();
    }

    public ObservableCollection<SlotItem> SlotItems { get; } = [];

    public void UpdateSlotLayout()
    {
        SlotLeftTopChildren = new ObservableCollection<SlotItem>(SlotItems.Where(item => item.Position == SlotPosition.LeftTop));
        SlotLeftBottumChildren = new ObservableCollection<SlotItem>(SlotItems.Where(item => item.Position == SlotPosition.LeftBottom));
        SlotRightTopChildren = new ObservableCollection<SlotItem>(SlotItems.Where(item => item.Position == SlotPosition.RightTop));
        SlotRightBottumChildren = new ObservableCollection<SlotItem>(SlotItems.Where(item => item.Position == SlotPosition.RightBottom));
        SlotBottumLeftChildren = new ObservableCollection<SlotItem>(SlotItems.Where(item => item.Position == SlotPosition.BottomLeft));
        SlotBottumRightChildren = new ObservableCollection<SlotItem>(SlotItems.Where(item => item.Position == SlotPosition.BottomRight));

        SlotLeftTopSelectedItem = SlotLeftTopChildren.FirstOrDefault();
        SlotLeftBottumSelectedItem = SlotLeftBottumChildren.FirstOrDefault();
        SlotRightTopSelectedItem = SlotRightTopChildren.FirstOrDefault();
        SlotRightBottumSelectedItem = SlotRightBottumChildren.FirstOrDefault();
        SlotBottumLeftSelectedItem = SlotBottumLeftChildren.FirstOrDefault();
        SlotBottumRightSelectedItem = SlotBottumRightChildren.FirstOrDefault();
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


    // 定义 SlotLeftBottum 的相关属性
    public static readonly StyledProperty<SlotItem?> SlotLeftBottumSelectedItemProperty =
        AvaloniaProperty.Register<LayoutControl, SlotItem?>(nameof(SlotLeftBottumSelectedItem), null,
            coerce: (avaloniaObject, control) => avaloniaObject is LayoutControl layoutControl
                ? layoutControl.SlotLeftBottumChildren.SingleOrDefault(c => c == control, null)
                : null);

    public static readonly StyledProperty<IList<SlotItem>> SlotLeftBottumChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, IList<SlotItem>>(nameof(SlotLeftBottumChildren), []);

    private SlotItem? SlotLeftBottumSelectedItem
    {
        get => GetValue(SlotLeftBottumSelectedItemProperty);
        set => SetValue(SlotLeftBottumSelectedItemProperty, value);
    }

    private IList<SlotItem> SlotLeftBottumChildren
    {
        get => GetValue(SlotLeftBottumChildrenProperty);
        set => SetValue(SlotLeftBottumChildrenProperty, value);
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

    // 定义 SlotRightBottum 的相关属性
    public static readonly StyledProperty<SlotItem?> SlotRightBottumSelectedItemProperty =
        AvaloniaProperty.Register<LayoutControl, SlotItem?>(nameof(SlotRightBottumSelectedItem), null,
            coerce: (avaloniaObject, control) => avaloniaObject is LayoutControl layoutControl
                ? layoutControl.SlotRightBottumChildren.SingleOrDefault(c => c == control, null)
                : null);

    public static readonly StyledProperty<IList<SlotItem>> SlotRightBottumChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, IList<SlotItem>>(nameof(SlotRightBottumChildren), []);

    private SlotItem? SlotRightBottumSelectedItem
    {
        get => GetValue(SlotRightBottumSelectedItemProperty);
        set => SetValue(SlotRightBottumSelectedItemProperty, value);
    }

    private IList<SlotItem> SlotRightBottumChildren
    {
        get => GetValue(SlotRightBottumChildrenProperty);
        set => SetValue(SlotRightBottumChildrenProperty, value);
    }

    // 定义 SlotBottumLeft 的相关属性
    public static readonly StyledProperty<SlotItem?> SlotBottumLeftSelectedItemProperty =
        AvaloniaProperty.Register<LayoutControl, SlotItem?>(nameof(SlotBottumLeftSelectedItem), null,
            coerce: (avaloniaObject, control) => avaloniaObject is LayoutControl layoutControl
                ? layoutControl.SlotBottumLeftChildren.SingleOrDefault(c => c == control, null)
                : null);

    public static readonly StyledProperty<IList<SlotItem>> SlotBottumLeftChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, IList<SlotItem>>(nameof(SlotBottumLeftChildren), []);

    private SlotItem? SlotBottumLeftSelectedItem
    {
        get => GetValue(SlotBottumLeftSelectedItemProperty);
        set => SetValue(SlotBottumLeftSelectedItemProperty, value);
    }

    private IList<SlotItem> SlotBottumLeftChildren
    {
        get => GetValue(SlotBottumLeftChildrenProperty);
        set => SetValue(SlotBottumLeftChildrenProperty, value);
    }

    // 定义 SlotBottumRight 的相关属性
    public static readonly StyledProperty<SlotItem?> SlotBottumRightSelectedItemProperty =
        AvaloniaProperty.Register<LayoutControl, SlotItem?>(nameof(SlotBottumRightSelectedItem), null,
            coerce: (avaloniaObject, control) => avaloniaObject is LayoutControl layoutControl
                ? layoutControl.SlotBottumRightChildren.SingleOrDefault(c => c == control, null)
                : null);

    public static readonly StyledProperty<IList<SlotItem>> SlotBottumRightChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, IList<SlotItem>>(nameof(SlotBottumRightChildren), []);

    private SlotItem? SlotBottumRightSelectedItem
    {
        get => GetValue(SlotBottumRightSelectedItemProperty);
        set => SetValue(SlotBottumRightSelectedItemProperty, value);
    }

    private IList<SlotItem> SlotBottumRightChildren
    {
        get => GetValue(SlotBottumRightChildrenProperty);
        set => SetValue(SlotBottumRightChildrenProperty, value);
    }

    // 新增 Content 属性
    public static readonly StyledProperty<Control?> ContentProperty =
        AvaloniaProperty.Register<LayoutControl, Control?>(nameof(Content), null);

    public Control? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }
}

public enum SlotPosition
{
    None = 0,
    LeftTop = 1,
    LeftBottom = 2,
    RightTop = 3,
    RightBottom = 4,
    BottomLeft = 5,
    BottomRight = 6
}

public class SlotItem
{
    public SlotPosition Position { get; set; }

    [Content] public required Control Content { get; init; }

    public required string Title { get; init; }

    public Geometry? IconPath { get; init; } = Geometry.Parse("M 40,20 L 60,40 L 40,60 L 20,40 Z");

    public double IconHeight { get; init; } = 20.0;

    public double IconWidth { get; init; } = 20.0;
}

public class SlotItemContentConverter : IValueConverter
{
    public static readonly SlotItemContentConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter,
        CultureInfo culture)
    {
        if (value is SlotItem item)
            return item.Content;
        else
            return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}