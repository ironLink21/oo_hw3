using System;

namespace AppLayer.Command
{
    public class CmdDeselectAll : Command
    {
        internal CmdDeselectAll() { }

        public override void Execute()
        {
            TargetDrawing?.DeselectAll();
        }

        public override void Undo()
        {
            TargetDrawing?.SelectAll();
        }
    }
}
