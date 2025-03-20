using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Metadata;

namespace DWK.Controls;

public class LayoutControl : TemplatedControl
{
    // 定义 SlotLeftTop 的相关属性
    public static readonly StyledProperty<SlotItem?> SlotLeftTopSelectedItemProperty =
        AvaloniaProperty.Register<LayoutControl, SlotItem?>(nameof(SlotLeftTopSelectedItem), null,
            coerce: (avaloniaObject, control) => avaloniaObject is LayoutControl layoutControl
                ? layoutControl.SlotLeftTopChildren.SingleOrDefault(c => c == control, null)
                : null);

    public static readonly StyledProperty<ObservableCollection<SlotItem>> SlotLeftTopChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, ObservableCollection<SlotItem>>(nameof(SlotLeftTopChildren), []);

    public SlotItem? SlotLeftTopSelectedItem
    {
        get => GetValue(SlotLeftTopSelectedItemProperty);
        set => SetValue(SlotLeftTopSelectedItemProperty, value);
    }

    public ObservableCollection<SlotItem> SlotLeftTopChildren
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

    public static readonly StyledProperty<ObservableCollection<SlotItem>> SlotLeftBottumChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, ObservableCollection<SlotItem>>(nameof(SlotLeftBottumChildren), []);

    public SlotItem? SlotLeftBottumSelectedItem
    {
        get => GetValue(SlotLeftBottumSelectedItemProperty);
        set => SetValue(SlotLeftBottumSelectedItemProperty, value);
    }

    public ObservableCollection<SlotItem> SlotLeftBottumChildren
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

    public static readonly StyledProperty<ObservableCollection<SlotItem>> SlotRightTopChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, ObservableCollection<SlotItem>>(nameof(SlotRightTopChildren), []);

    public SlotItem? SlotRightTopSelectedItem
    {
        get => GetValue(SlotRightTopSelectedItemProperty);
        set => SetValue(SlotRightTopSelectedItemProperty, value);
    }

    public ObservableCollection<SlotItem> SlotRightTopChildren
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

    public static readonly StyledProperty<ObservableCollection<SlotItem>> SlotRightBottumChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, ObservableCollection<SlotItem>>(nameof(SlotRightBottumChildren), []);

    public SlotItem? SlotRightBottumSelectedItem
    {
        get => GetValue(SlotRightBottumSelectedItemProperty);
        set => SetValue(SlotRightBottumSelectedItemProperty, value);
    }

    public ObservableCollection<SlotItem> SlotRightBottumChildren
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

    public static readonly StyledProperty<ObservableCollection<SlotItem>> SlotBottumLeftChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, ObservableCollection<SlotItem>>(nameof(SlotBottumLeftChildren), []);

    public SlotItem? SlotBottumLeftSelectedItem
    {
        get => GetValue(SlotBottumLeftSelectedItemProperty);
        set => SetValue(SlotBottumLeftSelectedItemProperty, value);
    }

    public ObservableCollection<SlotItem> SlotBottumLeftChildren
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

    public static readonly StyledProperty<ObservableCollection<SlotItem>> SlotBottumRightChildrenProperty =
        AvaloniaProperty.Register<LayoutControl, ObservableCollection<SlotItem>>(nameof(SlotBottumRightChildren), []);

    public SlotItem? SlotBottumRightSelectedItem
    {
        get => GetValue(SlotBottumRightSelectedItemProperty);
        set => SetValue(SlotBottumRightSelectedItemProperty, value);
    }

    public ObservableCollection<SlotItem> SlotBottumRightChildren
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

public class SlotItem
{
    [Content] public required Control Content { get; init; }

    public required string Title { get; init; }

    public Geometry? IconPath { get; init; } = Geometry.Parse("M 40,20 L 60,40 L 40,60 L 20,40 Z");

    public double IconHeight { get; init; } = 20.0;

    public double IconWidth { get; init; } = 20.0;
}    