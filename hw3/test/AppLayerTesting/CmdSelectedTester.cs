using System;
using System.Drawing;
using Xunit;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    public class CmdSelectedTester : testParent
    {
        [Fact]
        public void CmdSelected()
        {
            testSetup(false);
            Command selectedCmd = commandFactory.Create("SELECT", "Star-01", new Point(10,10), new Size(80, 80));
            Assert.NotNull(selectedCmd);

            selectedCmd.Execute();

            Star star = commandFactory.TargetDrawing.FindStarAtPosition(new Point(10,10));
            Assert.Equal(true, star.IsSelected);
        }

        [Fact]
        public void CmdSelected_undo() 
        {
            testSetup(false);
            // creates star obj and stores in variable
            Command selectedCmd = commandFactory.Create("SELECT", "Star-01", new Point(10,10), new Size(80, 80));

            // creates command to add the star
            selectedCmd.Execute();
            selectedCmd.Undo();

            Star star = commandFactory.TargetDrawing.FindStarAtPosition(new Point(10,10));
            Assert.Equal(false, star.IsSelected);
        }
    }
}