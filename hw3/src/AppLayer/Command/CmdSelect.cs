using System.Drawing;
using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    public class CmdSelect : Command
    {
        private readonly Point _location;
         
        internal CmdSelect(params object[] commandParameters)
        {
            if (commandParameters.Length>0)
            _location = (Point) commandParameters[0];
        }

        public override void Execute()
        {
            var star = TargetDrawing?.FindStarAtPosition(_location);
            if (star != null)
            {
                star.IsSelected = !star.IsSelected;
                TargetDrawing.IsDirty = true;
            }
        }

        public override void Undo()
        {
            // var star = TargetDrawing?.FindStarAtPosition(_location);
            // if (star != null)
            // {
            //     star.IsSelected = !star.IsSelected;
            //     TargetDrawing.IsDirty = true;
            // }
        }
    }
}
