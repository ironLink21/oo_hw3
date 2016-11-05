using System;
using System.Drawing;
using System.Collections.Generic;

using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    public class CmdScale : Command
    {
        private const int NormalWidth = 80;
        private const int NormalHeight = 80;

        private Point _location;
        private Size _size;
        private Size _newSize;
        internal CmdScale() {}

        /// <summary>
        /// Constructor
        /// 
        /// </summary>
        /// <param name="commandParameters">An array of parameters, where
        ///     [1]: Point  center location for the star, defaut = top left corner
        ///     [2]: float  scale factor
        ///     [3]: float  new scale factor</param>
        internal CmdScale(params object[] commandParameters)
        {
            if (commandParameters.Length > 2)
            {
                _location = (Point) commandParameters[0];

                float newScale = (float) commandParameters[1];

                var star = TargetDrawing?.FindStarAtPosition(_location);
                _size = star.Size;

                _newSize = new Size()
                {
                    Width = Convert.ToInt16(Math.Round(NormalWidth * newScale, 0)),
                    Height = Convert.ToInt16(Math.Round(NormalHeight * newScale, 0))
                };
            }
        }

        public override void Execute()
        {
            var star = TargetDrawing?.FindStarAtPosition(_location);
            if (star != null)
            {
                star.Size = _newSize;
                TargetDrawing.IsDirty = true;
            }
        }

        public override void Undo()
        {
            var star = TargetDrawing?.FindStarAtPosition(_location);
            if (star != null)
            {
                star.Size = _size;
                TargetDrawing.IsDirty = true;
            }
        }
    }
}