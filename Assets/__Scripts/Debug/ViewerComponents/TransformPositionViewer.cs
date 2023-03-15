public class TransformPositionViewer : DebugViewerComponent
{
    public override string ContainerName
    {
        get { return name + " Transform"; }
    }

    private enum DebugVariables
    {
        Position,
    }

    protected override void AddDebugVariablesToContainer()
    {
        container.Add((int)DebugVariables.Position, "Position ");
    }

    protected override void UpdateDebugVariablesValue()
    {
        container.UpdateVal((int)DebugVariables.Position, transform.position);
    }
}