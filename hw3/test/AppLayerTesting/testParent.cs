using System.Drawing;
using System.Collections.Generic;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    public class testParent
    {
        protected CommandFactory commandFactory = new CommandFactory();
        StarFactory starFactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdNewTester)};
        protected Drawing drawing = new Drawing();
        protected Star star;

        protected void testSetup(bool isMultiStars)
        {
            drawing = new Drawing() { IsDirty = false, Factory = starFactory};
            star = starFactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) });
            drawing.Add(star);

            if(isMultiStars)
            {
                star = starFactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(20,20), Size  = new Size(20, 20) });
                drawing.Add(star);

                star = starFactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(30,30), Size  = new Size(40, 40) });

                drawing.Add(star);
            }
            drawing.IsDirty = false;
        }
    }
}