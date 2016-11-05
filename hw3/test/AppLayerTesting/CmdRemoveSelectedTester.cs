using System;
using System.Drawing;
using Xunit;
using System.Collections.Generic;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    public class CmdRemoveSelectedTester
    {
        [Fact]
        private void function_runner()
        {
            CmdRemoveSelected();
            CmdRemoveSelected_UNDO();
        }

        public void CmdRemoveSelected()
        {
            List<Star> starList = new List<Star>();
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdRemoveSelectedTester)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80), IsSelected = true }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(200,310), Size = new Size(80, 80) }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(240,150), Size = new Size(80, 80) }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(350, 300), Size = new Size(80, 80) }));
            Assert.Equal(4, drawing.StarCount);

            Command removeSelectedCmd = commandFactory.Create("REMOVE");
            Assert.NotNull(removeSelectedCmd);

            removeSelectedCmd.Execute();

            // Assert the predicated results            
            starList = drawing.GetObjects(); 
            Assert.Equal(3, starList.Count);  
        }

        public void CmdRemoveSelected_UNDO()
        {
            List<Star> starList = new List<Star>();
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdRemoveSelectedTester)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(200,310), Size = new Size(80, 80) }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(240,150), Size = new Size(80, 80) }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(350, 300), Size = new Size(80, 80) }));
            Assert.Equal(4, drawing.StarCount);

            Command removeSelectedCmd = commandFactory.Create("REMOVE");
            Assert.NotNull(removeSelectedCmd);
            
            removeSelectedCmd.Execute();
            removeSelectedCmd.Undo();

            starList = drawing.GetObjects(); 
            Assert.Equal(4, starList.Count);
        }
    }
}