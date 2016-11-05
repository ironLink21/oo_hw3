using System;
using System.Drawing;
using Xunit;
using System.Collections.Generic;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    public class CmdDeselectAllTester
    {
        [Fact]
        private void function_runner()
        {
            CmdDeselectAll_DESELECT();
            CmdDeselectAll_DESELECT_UNDO();
        }
        
        public void CmdDeselectAll_DESELECT()
        {
            List<Star> starList = new List<Star>();
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdDeselectAll)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80), IsSelected = true }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(200,310), Size = new Size(80, 80), IsSelected = true }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(240,150), Size = new Size(80, 80), IsSelected = true }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(350, 300), Size = new Size(80, 80), IsSelected = true }));
            Assert.Equal(4, drawing.StarCount);

            Command deselectAllCmd = commandFactory.Create("DESELECT");
            Assert.NotNull(deselectAllCmd);

            // Stimulate (Execute newCmd.Execute)
            deselectAllCmd.Execute();

            // Assert the predicated results
            
            starList = drawing.GetSelected(); 
            Assert.Equal(0, starList.Count);
        }

        public void CmdDeselectAll_DESELECT_UNDO()
        {
            List<Star> starList = new List<Star>();
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdDeselectAll)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80), IsSelected = true }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(200,310), Size = new Size(80, 80), IsSelected = true }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(240,150), Size = new Size(80, 80), IsSelected = true }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(350, 300), Size = new Size(80, 80), IsSelected = true }));
            Assert.Equal(4, drawing.StarCount);

            Command deselectAllCmd = commandFactory.Create("DESELECT");
            Assert.NotNull(deselectAllCmd);

            // Stimulate (Execute newCmd.Execute)
            deselectAllCmd.Execute();
            deselectAllCmd.Undo();

            starList = drawing.GetSelected(); 
            Assert.Equal(4, starList.Count);
        }
    }
}