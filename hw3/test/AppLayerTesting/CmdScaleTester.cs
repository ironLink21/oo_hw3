using System;
using System.Drawing;
using Xunit;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    public class CmdScaleTester : testParent
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
            testSetup(false);
            Command scaleCmd = commandFactory.Create("SCALE", 2.00);
            Assert.NotNull(scaleCmd);

            scaleCmd.Execute();

            Star star = commandFactory.TargetDrawing.FindStarAtPosition(new Point(10,10));
            Assert.Equal(160, star.Size.Height);
            Assert.Equal(160, star.Size.Width);
        }

        public void CmdScale_null()
        {
            testSetup(false);
            Command scaleCmd = commandFactory.Create("SCALE");
            Assert.Equal(null, scaleCmd);
        }

        public void CmdScale_undo() 
        {
            testSetup(false);
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