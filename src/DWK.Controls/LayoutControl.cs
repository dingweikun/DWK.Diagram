using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using CommunityToolkit.Mvvm.Input;

namespace DWK.Controls;

public class LayoutControl : TemplatedControl
{
    public const double MinSpace = 40;

    private DockPanel? PART_NavbarLeftDockPanel, PART_NavbarRightDockPanel;

    public LayoutControl()
    {
        SlotItems.CollectionChanged += (_, _) =>
        {
            UpdateSlotChildren();
            UpdateNavbar();
            UpdateSlotLayout();
        };

        SlotItem.LayoutInstance = this;

        HideCommand = new RelayCommand<SlotItem>(item => SetSlotItemHidden(item, true));
        ShowCommand = new RelayCommand<SlotItem>(item => SetSlotItemHidden(item, false));
        MoveToLeftTopCommand = new RelayCommand<SlotItem>(item => SetSlotItemPosition(item, SlotPosition.LeftTop));
        MoveToLeftBottomCommand = new RelayCommand<SlotItem>(item => SetSlotItemPosition(item, SlotPosition.LeftBottom));
        MoveToRightTopCommand = new RelayCommand<SlotItem>(item => SetSlotItemPosition(item, SlotPosition.RightTop));
        MoveToRightBottomCommand = new RelayCommand<SlotItem>(item => SetSlotItemPosition(item, SlotPosition.RightBottom));
        MoveToBottomLeftCommand = new RelayCommand<SlotItem>(item => SetSlotItemPosition(item, SlotPosition.BottomLeft));
        MoveToBottomRightCommand = new RelayCommand<SlotItem>(item => SetSlotItemPosition(item, SlotPosition.BottomRight));
        MoveMenuToLeftCommand = new RelayCommand(() => SetMenuSide(false));
        MoveMenuToRightCommand = new RelayCommand(() => SetMenuSide(true));
    }

    #region 属性

    public ObservableCollection<SlotItem> SlotItems { get; } = [];

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

    // 布局菜单是否在右侧
    public static readonly DirectProperty<LayoutControl, bool> MenuOnRightProperty = AvaloniaProperty.RegisterDirect<LayoutControl, bool>(
        nameof(MenuOnRight),
        o => o.MenuOnRight,
        (o, v) => o.MenuOnRight = v);

    private bool _menuOnRight;

    public bool MenuOnRight
    {
        get => _menuOnRight;
        set => SetAndRaise(MenuOnRightProperty, ref _menuOnRight, value);
    }

    #endregion

    #region 命令

    public ICommand HideCommand { get; }
    public ICommand ShowCommand { get; }
    public ICommand MoveToLeftTopCommand { get; }
    public ICommand MoveToLeftBottomCommand { get; }
    public ICommand MoveToRightTopCommand { get; }
    public ICommand MoveToRightBottomCommand { get; }
    public ICommand MoveToBottomLeftCommand { get; }
    public ICommand MoveToBottomRightCommand { get; }
    public ICommand MoveMenuToLeftCommand { get; }
    public ICommand MoveMenuToRightCommand { get; }

    #endregion

    private void UpdateSlotChildren()
    {
        var ltSel = SlotLeftTopSelectedItem;
        var lbSel = SlotLeftBottomSelectedItem;
        var rtSel = SlotRightTopSelectedItem;
        var rbSel = SlotRightBottomSelectedItem;
        var blSel = SlotBottomLeftSelectedItem;
        var brSel = SlotBottomRightSelectedItem;

        List<SlotItem> hidden = [], lt = [], lb = [], rt = [], rb = [], bl = [], br = [];

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

        SlotLeftTopSelectedItem = ltSel;
        SlotLeftBottomSelectedItem = lbSel;
        SlotRightTopSelectedItem = rtSel;
        SlotRightBottomSelectedItem = rbSel;
        SlotBottomLeftSelectedItem = blSel;
        SlotBottomRightSelectedItem = brSel;
    }

    private void SetSlotItemHidden(SlotItem? slotItem, bool hidden)
    {
        if (slotItem is null || slotItem.IsHidden == hidden) return;

        slotItem.IsHidden = hidden;
        UpdateSlotChildren();
        UpdateNavbar();

        if (!slotItem.IsHidden)
            SetSlotSelectedItem(slotItem.Position, slotItem);
    }

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
        UpdateSlotChildren();
        UpdateNavbar();

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

    private void SetMenuSide(bool menuOnRight)
    {
        MenuOnRight = menuOnRight;
        UpdateNavbar();
    }

    private void UpdateNavbar()
    {
        if (PART_NavbarLeftDockPanel != null)
            PART_NavbarLeftDockPanel.IsVisible =
                (HiddenSlotItems.Count > 0 && !MenuOnRight) ||
                SlotLeftTopChildren.Count > 0 ||
                SlotLeftBottomChildren.Count > 0 ||
                SlotBottomLeftChildren.Count > 0;

        if (PART_NavbarRightDockPanel != null)
            PART_NavbarRightDockPanel.IsVisible =
                (HiddenSlotItems.Count > 0 && MenuOnRight) ||
                SlotRightTopChildren.Count > 0 ||
                SlotRightBottomChildren.Count > 0 ||
                SlotBottomRightChildren.Count > 0;
    }


    private void UpdateSlotLayout()
    {
    }


    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        PART_NavbarLeftDockPanel = e.NameScope.Find<DockPanel>(nameof(PART_NavbarLeftDockPanel)) ?? throw new NotSupportedException();
        PART_NavbarRightDockPanel = e.NameScope.Find<DockPanel>(nameof(PART_NavbarRightDockPanel)) ?? throw new NotSupportedException();
    }


    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == SlotLeftTopSelectedItemProperty || change.Property == SlotLeftBottomSelectedItemProperty ||
            change.Property == SlotRightTopSelectedItemProperty || change.Property == SlotRightBottomSelectedItemProperty ||
            change.Property == SlotBottomLeftSelectedItemProperty || change.Property == SlotBottomRightSelectedItemProperty)
        {
            if (change.NewValue is not null && change.OldValue is not null)
                return; // 窗口内容替换，布局不需要更新

            UpdateSlotLayout();
        }
    }
}
