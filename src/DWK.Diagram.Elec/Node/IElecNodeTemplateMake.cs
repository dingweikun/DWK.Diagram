namespace DWK.Diagram.Node;

internal interface IElecNodeTemplateMake<TNodeData> where TNodeData : ElecNodeData
{
    TNodeData NodeDataSample { get; }

    Northwoods.Go.Node Make();
}