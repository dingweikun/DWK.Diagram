namespace DWK.Diagram.Views;

using GoPalette = Northwoods.Go.Palette;

public partial class PaletteView : UserControl
{
    public PaletteView()
    {
        InitializeComponent();

        var palette = PART_PaletteControl.Diagram as GoPalette;
        var builder = new ElecPaletteBuilder();
        builder.BuildPalette(palette);
    }

    // private void SetupPalette(GoPalette _Palette)
    // {
    //     DefineNodeTemplates();
    //     _Palette.NodeTemplateMap = sharedNodeTemplateMap;
    //
    //     _Palette.Model = new Model
    //     {
    //         NodeDataSource = new List<NodeData>
    //         {
    //             new NodeData { Category = "Input" },
    //             new NodeData { Category = "Output" },
    //             new NodeData { Category = "And" },
    //             new NodeData { Category = "Or" },
    //             new NodeData { Category = "Xor" },
    //             new NodeData { Category = "Not" },
    //             new NodeData { Category = "Nand" },
    //             new NodeData { Category = "Nor" },
    //             new NodeData { Category = "Xnor" }
    //         }
    //     };
    // }
}