using System;
using System.Drawing;
using System.Collections.Generic;

using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    public class CmdMove : Command
    {
        private Point _location;
        private Point _newLocation;
        internal CmdMove() {}

        /// <summary>
        /// Constructor
        /// 
        /// </summary>
        /// <param name="commandParameters">An array of parameters, where
        ///     [1]: Point      center location for the star, defaut = top left corner
        ///     [2]: NewPoint   new center location for the star, defaut = top left corner</param>
        internal CmdMove(params object[] commandParameters)
        {
            if (commandParameters.Length > 1)
            {
                _location = (Point) commandParameters[0];
                _newLocation = (Point) commandParameters[1];
            }
        }

        public override void Execute()
        {
            var star = TargetDrawing?.FindStarAtPosition(_location);
            if (star != null)
            {
                star.Location = _newLocation;
                TargetDrawing.IsDirty = true;
            }
        }

        public override void Undo()
        {
            var star = TargetDrawing?.FindStarAtPosition(_newLocation);
            if (star != null)
            {
                star.Location = _location;
                TargetDrawing.IsDirty = true;
            }
        }
    }
}