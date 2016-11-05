using System;
using System.Drawing;
using Xunit;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace AppLayerTesting
{
    public class CmdAddTester
    {
        protected CommandFactory commandFactory = new CommandFactory();
        StarFactory starFactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdAddTester)};
        protected Drawing drawing = new Drawing();
        protected Star star;

        [Fact]
        private void function_runner()
        {
            CmdAdd_add_not_null();
            CmdAdd_add_null();
            CmdAdd_undo();
        }
        
        public void CmdAdd_add_not_null()
        {
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdAddTester)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) }));
            Assert.Equal(1, drawing.StarCount);

            Command addCmd = commandFactory.Create("ADD", "Star-01");
            Assert.NotNull(addCmd);

            addCmd.Execute();

            Assert.Equal(2, drawing.StarCount);
        }

        public void CmdAdd_add_null()
        {
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdAddTester)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) }));
            Assert.Equal(1, drawing.StarCount);

            // creates star obj and stores in variable
            Star tempStar = Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) });

            // creates command to add the star
            Command addCmd = commandFactory.Create("ADD");
            Assert.Equal(null, addCmd);
        }

        public void CmdAdd_undo()
        {
            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdAddTester)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) }));
            Assert.Equal(1, drawing.StarCount);

            Command addCmd = commandFactory.Create("ADD", "Star-01",new Point(10,10), 1.0);

            addCmd.Execute();

            addCmd.Undo();

            Assert.Equal(1, drawing.StarCount);
        }
    }
}