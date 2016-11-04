using System;
using System.Drawing;
using Xunit;
using System.Collections.Generic;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    public class CmdRemoveSelectedTester : testParent
    {
        [Fact]
        public void CmdRemoveSelected()
        {
            List<Star> starList = new List<Star>();
            testSetup(true);

            Command removeSelectedCmd = commandFactory.Create("REMOVE");
            Assert.NotNull(removeSelectedCmd);

            removeSelectedCmd.Execute();

            // Assert the predicated results            
            starList = drawing.GetSelected(); 
            Assert.Equal(0, starList.Count);  
        }

        [Fact]
        public void CmdRemoveSelected_UNDO()
        {
            List<Star> starList = new List<Star>();
            testSetup(true);

            Command removeSelectedCmd = commandFactory.Create("REMOVE");
            Assert.NotNull(removeSelectedCmd);

            // Stimulate (Execute newCmd.Execute)
            removeSelectedCmd.Execute();
            starList = drawing.GetSelected(); 
            Assert.Equal(0, starList.Count);

            // Assert the predicated results
            
            removeSelectedCmd.Undo();

            starList = drawing.GetSelected(); 
            Assert.Equal(0, starList.Count);
        }
    }
}