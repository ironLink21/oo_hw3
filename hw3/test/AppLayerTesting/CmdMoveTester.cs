using System;
using System.Drawing;
using Xunit;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    public class CmdMoveTester
    {
        [Fact]
        private void function_runner()
        {
            CmdMove_norm();
            CmdMove_null();
            CmdMove_undo();
        }

        public void CmdMove_norm()
        {
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdMoveTester)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) }));
            Assert.Equal(1, drawing.StarCount);

            Command moveCmd = commandFactory.Create("MOVE", new Point(10,10), new Point(30,30));
            Assert.NotNull(moveCmd);

            moveCmd.Execute();

            Star star = commandFactory.TargetDrawing.FindStarAtPosition(new Point(30,30));
            Assert.NotNull(star);
        }

        public void CmdMove_null()
        {
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdMoveTester)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) }));
            Assert.Equal(1, drawing.StarCount);

            Command moveCmd = commandFactory.Create("MOVE");
            Assert.Equal(null, moveCmd);
        }

        public void CmdMove_undo() 
        {
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdMoveTester)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) }));
            Assert.Equal(1, drawing.StarCount);

            // creates star obj and stores in variable
            Command moveCmd = commandFactory.Create("MOVE", new Point(10,10), new Point(30,30));

            // creates command to add the star
            moveCmd.Execute();
            moveCmd.Undo();

            Star star = commandFactory.TargetDrawing.FindStarAtPosition(new Point(10,10));
            Assert.NotNull(star);
        }
    }
}