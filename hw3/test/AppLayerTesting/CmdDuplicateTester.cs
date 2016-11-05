using System;
using System.Drawing;
using Xunit;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    public class CmdDuplicateTester : testParent
    {
        [Fact]
        private void function_runner()
        {
            CmdDuplicate_norm();
            CmdDuplicate_null();
            CmdDuplicate_undo();
        }

        public void CmdDuplicate_norm()
        {
            testSetup(false);
            Command duplicateCmd = commandFactory.Create("DUPLICATE", new Point(10,10));
            Assert.NotNull(duplicateCmd);

            duplicateCmd.Execute();

            Star star = commandFactory.TargetDrawing.FindStarAtPosition(new Point(10,10));
            Assert.NotNull(star);
        }

        public void CmdDuplicate_null()
        {
            testSetup(false);
            Command duplicateCmd = commandFactory.Create("DUPLICATE");
            Assert.Equal(null, duplicateCmd);
        }

        public void CmdDuplicate_undo() 
        {
            testSetup(false);
            // creates star obj and stores in variable
            Command duplicateCmd = commandFactory.Create("DUPLICATE", new Point(10,10));

            // creates command to add the star
            duplicateCmd.Execute();
            duplicateCmd.Undo();

            Star star = commandFactory.TargetDrawing.FindStarAtPosition(new Point(10,10));
            Assert.NotNull(star);
        }
    }
}