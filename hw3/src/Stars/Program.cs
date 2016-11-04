using System;

using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace Space
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Invoker invoker = new Invoker();
            invoker.Start();

            StarFactory Starfactory = new StarFactory() {ResourceNamePattern = "AppLayerTesting.Graphics.{0}.png", ReferenceType = typeof(CmdNewTester)};
            Drawing drawing = new Drawing();
            CommandFactory commandFactory = new CommandFactory() {TargetDrawing = drawing};
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(10,10), Size  = new Size(80, 80) }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(200,310), Size = new Size(80, 80) }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(240,150), Size = new Size(80, 80) }));
            drawing.Add(Starfactory.GetStar(new StarExtrinsicState() { StarType = "Star-01", Location = new Point(350, 300), Size = new Size(80, 80) }));

            Command newCmd = commandFactory.Create("NEW");
            invoker.EnqueueCmd(newCmd)

            string type = "Star-01";
            Point point = new Point(10,10);
            Size size = new Size(80, 80);

            // creates command to add the star
            Command addCmd = commandFactory.Create("ADD", type, point, size);
            invoker.EnqueueCmd(addCmd)


        }
    }
}
