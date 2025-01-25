namespace DWK.Diagram.Node;

public class GeneratorNodeData : ElecNodeData, IResizedWidth
{
    public GeneratorNodeData()
    {
        base.Category = Tag = ElecNodeCategory.Generator;
    }

    public double ResizedWidth { get; set; } = GeneratorNodeTemplate.DefaultWidth;
}