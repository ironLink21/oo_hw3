using System;
using System.Drawing;
using Xunit;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    public class CmdNewTester
    {
         [Fact]
        public void CmdNew_NonEmptyDrawing()
        {
            // Setup a drawing
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdNewTester)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(200,310), Size = new Size(80, 80) }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(240,150), Size = new Size(80, 80) }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(350, 300), Size = new Size(80, 80) }));
            Assert.Equal(4, drawing.StarCount);

            // setup a New command
            Command newCmd = commandFactory.Create("NEW");
            Assert.NotNull(newCmd);

            // Stimulate (Execute newCmd.Execute)
            newCmd.Execute();

            // Assert the predicated results
            Assert.Equal(0, drawing.StarCount);            
        }

         [Fact]
        public void CmdNew_EmptyDrawing()
        {
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() { TargetDrawing = drawing };

            Assert.Equal(0, drawing.StarCount);

            Command newCmd = commandFactory.Create("NEW");
            Assert.NotNull(newCmd);
            newCmd.Execute();
            Assert.Equal(0, drawing.StarCount);
        }

         [Fact]
        public void CmdNew_NoDrawing()
        {
            CommandFactory commandFactory = new CommandFactory() { TargetDrawing = null };

            Command newCmd = commandFactory.Create("NEW");
            Assert.NotNull(newCmd);
            newCmd.Execute();
            // This didn't throw an exception, then it worked as expected
        }
    }
}
