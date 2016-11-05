using System;
using System.Drawing;
using System.Collections.Generic;

using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    public class CmdAdd : Command
    {
        private const int NormalWidth = 80;
        private const int NormalHeight = 80;

        private readonly string _starType;
        private Point _location;
        private readonly float _scale;
        internal CmdAdd() {}

        /// <summary>
        /// Constructor
        /// 
        /// </summary>
        /// <param name="commandParameters">An array of parameters, where
        ///     [1]: string     star type -- a fully qualified resource name
        ///     [2]: Point      center location for the star, defaut = top left corner
        ///     [3]: float      scale factor</param>
        internal CmdAdd(params object[] commandParameters)
        {
            if (commandParameters.Length>0)
            {
                _starType = commandParameters[0] as string;
            }

            if (commandParameters.Length > 1)
            {
                _location = (Point) commandParameters[1];
            }
            else
            {
                _location = new Point(0, 0);
            }

            if (commandParameters.Length > 2)
            {
                _scale = (float) commandParameters[2];
            }
            else
            {
                _scale = 1.0F;
            }
        }

        public override void Execute()
        {
            if (string.IsNullOrWhiteSpace(_starType) || TargetDrawing==null) return;

            Size starSize = new Size()
            {
                Width = Convert.ToInt16(Math.Round(NormalWidth * _scale, 0)),
                Height = Convert.ToInt16(Math.Round(NormalHeight * _scale, 0))
            };

            Point starLocation = new Point(_location.X - starSize.Width / 2, _location.Y - starSize.Height / 2);

            StarExtrinsicState extrinsicState = new StarExtrinsicState()
            {
                StarType = _starType,
                Location = starLocation,
                Size = starSize
            };

            var star = TargetDrawing.Factory.GetStar(extrinsicState);

            TargetDrawing.Add(star);
        }

        public override void Undo()
        {
            if (TargetDrawing==null) return;
            
            TargetDrawing.RemoveLastStar();
        }
    }
}
