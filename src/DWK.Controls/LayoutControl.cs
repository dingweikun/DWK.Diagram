using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media;

namespace DWK.Controls;


[TemplatePart("PART_CCC", typeof(Button), IsRequired = true)]
public class LayoutControl : TemplatedControl
{
    private Border? PART_CCC;
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        PART_CCC = e.NameScope.Get<Border>("PART_BBB");

        var btn = e.NameScope.Get<Button>("PART_CCC");
        btn.Content = "Hello";
        btn.Click += BtnOnClick;

        Control c = new();
        
    }
    

    private void BtnOnClick(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Hello" + this is ILogical);

        // if (PART_CCC is null) return;
        PART_CCC.Background = new SolidColorBrush(Colors.Orange);
    }
}

public record LayoutItem
{
    public required string Title { get; init; }
    public required PathIcon Icon { get; init; }
    public required Control Content { get; init; }
}