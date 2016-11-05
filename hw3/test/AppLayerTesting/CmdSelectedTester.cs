using System;
using System.Drawing;
using Xunit;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    public class CmdSelectedTester
    {
        [Fact]
        private void function_runner()
        {
            CmdSelected();
            CmdSelected_undo();
        }
        
        public void CmdSelected()
        {
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdSelectedTester)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) }));
            Assert.Equal(1, drawing.StarCount);

            Command selectedCmd = commandFactory.Create("SELECT", "Star-01", new Point(10,10));
            Assert.NotNull(selectedCmd);

            selectedCmd.Execute();

            Star star = commandFactory.TargetDrawing.FindStarAtPosition(new Point(10,10));
            Assert.Equal(true, star.IsSelected);
        }

        public void CmdSelected_undo() 
        {
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdSelectedTester)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) }));
            Assert.Equal(1, drawing.StarCount);

            // creates star obj and stores in variable
            Command selectedCmd = commandFactory.Create("SELECT", "Star-01", new Point(10,10));

            // creates command to add the star
            selectedCmd.Execute();
            selectedCmd.Undo();

            Star star = commandFactory.TargetDrawing.FindStarAtPosition(new Point(10,10));
            Assert.Equal(false, star.IsSelected);
        }        
    }
}