using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace DWK.Controls;

public class LayoutControl : TemplatedControl
{
    public static readonly StyledProperty<Control?> SlotLeftSelectedItemProperty =
        AvaloniaProperty.Register<LayoutControl, Control?>(nameof(SlotLeftSelectedItem), null,
            coerce: (avaloniaObject, control) => avaloniaObject is LayoutControl layoutControl
                ? layoutControl.Slot1Children.SingleOrDefault(c => c == control, null)
                : null);

    public static readonly StyledProperty<Control?> SlotRightSelectedItemProperty =
        AvaloniaProperty.Register<LayoutControl, Control?>(nameof(SlotRightSelectedItem), null,
            coerce: (avaloniaObject, control) => avaloniaObject is LayoutControl layoutControl
                ? layoutControl.Slot2Children.SingleOrDefault(c => c == control, null)
                : null);

    public static readonly AttachedProperty<ObservableCollection<Control>> Slot1ChildrenProperty =
        AvaloniaProperty.RegisterAttached<LayoutControl, Control, ObservableCollection<Control>>(nameof(Slot1Children), []);

    public static readonly AttachedProperty<ObservableCollection<Control>> Slot2ChildrenProperty =
        AvaloniaProperty.RegisterAttached<LayoutControl, Control, ObservableCollection<Control>>(nameof(Slot2Children), []);

    public Control? SlotLeftSelectedItem
    {
        get => GetValue(SlotLeftSelectedItemProperty);
        set => SetValue(SlotLeftSelectedItemProperty, value);
    }

    public Control? SlotRightSelectedItem
    {
        get => GetValue(SlotRightSelectedItemProperty);
        set => SetValue(SlotRightSelectedItemProperty, value);
    }

    public ObservableCollection<Control> Slot1Children
    {
        get => GetValue(Slot1ChildrenProperty);
        set => SetValue(Slot1ChildrenProperty, value);
    }

    public ObservableCollection<Control> Slot2Children
    {
        get => GetValue(Slot2ChildrenProperty);
        set => SetValue(Slot2ChildrenProperty, value);
    }
}