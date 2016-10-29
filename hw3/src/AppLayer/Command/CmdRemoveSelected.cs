namespace AppLayer.Command
{
    public class CmdRemoveSelected : Command
    {
        internal CmdRemoveSelected() { }

        public override void Execute()
        {
            TargetDrawing?.DeleteAllSelected();
        }

        public override void Undo()
        {
            // TargetDrawing?.DeleteAllSelected();
        }
    }
}
