using System;
using System.Drawing;
using System.Collections.Generic;

using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    public class CmdRemoveSelected : Command
    {
        private List<Star> deletedStars;
        internal CmdRemoveSelected() { }

        public override void Execute()
        {
            deletedStars = TargetDrawing?.DeleteAllSelected();
        }

        public override void Undo()
        {
            if(deletedStars != null)
            {
                foreach (Star item in deletedStars)
                {
                    TargetDrawing.Add(item);
                }
            }
        }
    }
}
