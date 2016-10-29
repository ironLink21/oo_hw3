using System;

namespace AppLayer.Command
{
    public class CmdNew : Command
    {
        internal CmdNew() {}

        public override void Execute()
        {
            TargetDrawing?.Clear();
        }

        public override void Undo()
        {
            // TargetDrawing?.Clear();
        }
    }
}
