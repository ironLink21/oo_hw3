using System;
using System.Drawing;
using Xunit;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    public class CmdAddTester : testParent
    {
        [Fact]
        public void CmdAdd_add_not_null()
        {
            testSetup(false);
            Command addCmd = commandFactory.Create("ADD", "Star-01");
            Assert.NotNull(addCmd);

            addCmd.Execute();

            Assert.Equal(1, drawing.StarCount);
        }

        [Fact]
        public void CmdAdd_add_null() 
        {
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdAddTester)};

            // creates star obj and stores in variable
            Star tempStar = Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) });

            // creates command to add the star
            Command addCmd = commandFactory.Create("ADD");
            Assert.Equal(null, addCmd);
        }

        [Fact]
        public void CmdAdd_undo()
        {
            testSetup(false);
            Command addCmd = commandFactory.Create("ADD", "Star-01");

            addCmd.Execute();

            addCmd.Undo();

            Assert.Equal(2, drawing.StarCount);
        }
    }
}