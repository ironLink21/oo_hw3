using System.Drawing;
using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    public class SelectCommand : Command
    {
        private readonly Point _location;
         
        internal SelectCommand(params object[] commandParameters)
        {
            if (commandParameters.Length>0)
            _location = (Point) commandParameters[0];
        }

        public override void Execute()
        {
            var tree = TargetDrawing?.FindTreeAtPosition(_location);
            if (tree != null)
            {
                tree.IsSelected = !tree.IsSelected;
                TargetDrawing.IsDirty = true;
            }
        }
    }
}
