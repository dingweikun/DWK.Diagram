using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Metadata;

namespace DWK.Controls;

public enum SlotPosition
{
    LeftTop,
    LeftBottom,
    RightTop,
    RightBottom,
    BottomLeft,
    BottomRight
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