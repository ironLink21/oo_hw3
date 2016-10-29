using System.Drawing;

namespace AppLayer.DrawingComponents
{
    /// <summary>
    /// This class plays a role in two different patterns: a Flyweight and a Decorator.  For the Flyweight, this
    /// class represent a "whole" star that combines both intrinsic state part and the extrinsic state part.
    /// Objects of this class only need to exist for short period time, like a drawing session.
    /// 
    /// For the decorator pattern, this class is a Decorator.  It add the extrinsic state to StarWithIntrinsic State objects
    /// </summary>
    public class StarWithAllState : Star
    {
        internal StarWithIntrinsicState IntrinsicState { get; }
        public StarExtrinsicState ExtrinsicState { get; }

        internal StarWithAllState(StarWithIntrinsicState sharedPart, StarExtrinsicState nonsharedPart)
        {
            IntrinsicState = sharedPart;                // From a decorator perspective, this is the decorated object
            ExtrinsicState = nonsharedPart;            // From a decorator perspective, this is the added feature or
                                                        // capability that this object (a decorator) is adding
        }

        public override bool IsSelected {
            get { return ExtrinsicState.IsSelected;  }
            set { ExtrinsicState.IsSelected = value;  }
        }

        public override Point Location
        {
            get { return ExtrinsicState.Location; }
            set { ExtrinsicState.Location = value; }
        }


        public override Size Size
        {
            get { return ExtrinsicState.Size; }
            set { ExtrinsicState.Size = value; }
        }

        public override void Draw(Graphics graphics)
        {
            if (graphics == null || IntrinsicState?.Image == null) return;

            graphics.DrawImage(IntrinsicState.Image,
                new Rectangle(ExtrinsicState.Location.X, ExtrinsicState.Location.Y, ExtrinsicState.Size.Width, ExtrinsicState.Size.Height),
                0, 0, IntrinsicState.Image.Width, IntrinsicState.Image.Height,
                GraphicsUnit.Pixel);

            if (ExtrinsicState.IsSelected)
            {
                graphics.DrawRectangle(
                    SelectedPen,
                    ExtrinsicState.Location.X,
                    ExtrinsicState.Location.Y,
                    ExtrinsicState.Size.Width,
                    ExtrinsicState.Size.Height);

                DrawActionHandle(graphics, ExtrinsicState.Location.X, ExtrinsicState.Location.Y);
                DrawActionHandle(graphics, ExtrinsicState.Location.X + ExtrinsicState.Size.Width, ExtrinsicState.Location.Y);
                DrawActionHandle(graphics, ExtrinsicState.Location.X, ExtrinsicState.Location.Y + ExtrinsicState.Size.Height);
                DrawActionHandle(graphics, ExtrinsicState.Location.X + ExtrinsicState.Size.Width, ExtrinsicState.Location.Y + ExtrinsicState.Size.Height);
            }
        }

        private void DrawActionHandle(Graphics graphics, int x, int y)
        {
            graphics.FillRectangle(HandlesBrush, x - HandleHalfSize, y - HandleHalfSize, HandleHalfSize*2, HandleHalfSize*2);
        }
    }
}
