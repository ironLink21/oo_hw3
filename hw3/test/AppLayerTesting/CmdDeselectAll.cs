using System;
using System.Drawing;
using Xunit;
using System.Collections.Generic;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    public class CmdDeselectAll : testParent
    {
        [Fact]
        public void CmdDeselectAll_DESELECT()
        {
            List<Star> starList = new List<Star>();
            testSetup(true);

            Command deselectAllCmd = commandFactory.Create("DESELECT");
            Assert.NotNull(deselectAllCmd);

            // Stimulate (Execute newCmd.Execute)
            deselectAllCmd.Execute();

            // Assert the predicated results
            
            starList = drawing.GetSelected(); 
            Assert.Equal(0, starList.Count);
        }

        [Fact]
        public void CmdDeselectAll_DESELECT_UNDO()
        {
            List<Star> starList = new List<Star>();
            testSetup(true);

            Command deselectAllCmd = commandFactory.Create("DESELECT");
            Assert.NotNull(deselectAllCmd);

            // Stimulate (Execute newCmd.Execute)
            deselectAllCmd.Execute();

            // Assert the predicated results
            starList = drawing.GetSelected(); 
            Assert.Equal(0, starList.Count);

            deselectAllCmd.Undo();
            starList = drawing.GetSelected(); 
            Assert.Equal(0, starList.Count);
        }
    }
}