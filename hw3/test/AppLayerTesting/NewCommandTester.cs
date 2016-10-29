using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    [TestClass]
    public class CmdNewTester
    {
        [TestMethod]
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
            Assert.AreEqual(4, drawing.StarCount);

            // setup a New command
            Command newCmd = commandFactory.Create("new");
            Assert.IsNotNull(newCmd);

            // Stimulate (Execute newCmd.Execute)
            newCmd.Execute();

            // Assert the predicated results
            Assert.AreEqual(0, drawing.StarCount);            
        }

        [TestMethod]
        public void CmdNew_EmptyDrawing()
        {
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() { TargetDrawing = drawing };

            Assert.AreEqual(0, drawing.StarCount);

            Command newCmd = commandFactory.Create("new");
            Assert.IsNotNull(newCmd);
            newCmd.Execute();
            Assert.AreEqual(0, drawing.StarCount);

        }

        [TestMethod]
        public void CmdNew_NoDrawing()
        {
            CommandFactory commandFactory = new CommandFactory() { TargetDrawing = null };

            Command newCmd = commandFactory.Create("new");
            Assert.IsNotNull(newCmd);
            newCmd.Execute();
            // This didn't throw an exception, then it worked as expected
        }

    }
}
