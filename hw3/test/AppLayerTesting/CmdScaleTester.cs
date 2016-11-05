using System;
using System.Drawing;
using Xunit;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    public class CmdScaleTester
    {
        [Fact]
        private void function_runner()
        {
            CmdScale_norm();
            CmdScale_null();
            CmdScale_undo();
        }

        public void CmdScale_norm()
        {
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdScaleTester)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) }));
            Assert.Equal(1, drawing.StarCount);

            Command scaleCmd = commandFactory.Create("SCALE", 2.00);
            Assert.NotNull(scaleCmd);

            scaleCmd.Execute();

            Star star = commandFactory.TargetDrawing.FindStarAtPosition(new Point(10,10));
            Assert.Equal(160, star.Size.Height);
            Assert.Equal(160, star.Size.Width);
        }

        public void CmdScale_null()
        {
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdScaleTester)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) }));
            Assert.Equal(1, drawing.StarCount);

            Command scaleCmd = commandFactory.Create("SCALE");
            Assert.Equal(null, scaleCmd);
        }

        public void CmdScale_undo() 
        {
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdScaleTester)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) }));
            Assert.Equal(1, drawing.StarCount);

            // creates star obj and stores in variable
            Command scaleCmd = commandFactory.Create("SCALE", 2.00);

            // creates command to add the star
            scaleCmd.Execute();
            scaleCmd.Undo();

            Star star = commandFactory.TargetDrawing.FindStarAtPosition(new Point(10,10));
            Assert.Equal(80, star.Size.Height);
            Assert.Equal(80, star.Size.Width);
        }
    }
}