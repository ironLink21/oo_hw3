using System;
using System.Drawing;
using Xunit;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    public class CmdMoveTester : testParent
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
            testSetup(false);
            Command moveCmd = commandFactory.Create("MOVE", new Point(10,10), new Point(30,30));
            Assert.NotNull(moveCmd);

            moveCmd.Execute();

            Star star = commandFactory.TargetDrawing.FindStarAtPosition(new Point(30,30));
            Assert.NotNull(star);
        }

        public void CmdMove_null()
        {
            testSetup(false);
            Command moveCmd = commandFactory.Create("MOVE");
            Assert.Equal(null, moveCmd);
        }

        public void CmdMove_undo() 
        {
            testSetup(false);
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