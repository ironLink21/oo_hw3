using System.IO;
using System.Runtime.Serialization.Json;
using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    public class CmdSave : Command
    {
        private readonly string _filename;
        internal CmdSave(params object[] commandParameters)
        {
            if (commandParameters.Length > 0)
                _filename = commandParameters[0] as string;
        }

        public override void Execute()
        {
            StreamWriter writer = new StreamWriter(_filename);
            TargetDrawing?.SaveToStream(writer.BaseStream);
            writer.Close();
        }

        public override void Undo()
        {
            // StreamWriter writer = new StreamWriter(_filename);
            // TargetDrawing?.SaveToStream(writer.BaseStream);
            // writer.Close();
        }
    }
}
