using System;
using System.Drawing;
using System.Collections.Generic;

using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    public class CmdDuplicate : Command
    {
        private Point _location;
        internal CmdDuplicate() {}

        /// <summary>
        /// Constructor
        /// 
        /// </summary>
        /// <param name="commandParameters">An array of parameters, where
        ///     [1]: Point  center location for the star, defaut = top left corner</param>
        internal CmdDuplicate(params object[] commandParameters)
        {
            if (commandParameters.Length > 0)
            {
                _location = (Point) commandParameters[0];
            }
        }

        public override void Execute()
        {
            var star = TargetDrawing?.FindStarAtPosition(_location);
            if (star != null)
            {
                // this creates a new point offset from the current location by 10 (should be lower and to the right)
                _location = new Point((star.Location.X - star.Size.Width / 2) + 10, (star.Location.Y - star.Size.Height / 2) + 10);

                StarExtrinsicState extrinsicState = new StarExtrinsicState()
                {
                    StarType = TargetDrawing.Factory.StarType,
                    Location = _location,
                    Size = star.Size,
                    IsSelected = true
                };

                var starDopple = TargetDrawing.Factory.GetStar(extrinsicState);

                TargetDrawing.Add(starDopple);
                
                TargetDrawing.IsDirty = true;
            }
        }

        public override void Undo()
        {
            TargetDrawing.RemoveLastStar();
            TargetDrawing.IsDirty = true;
        }
    }
}