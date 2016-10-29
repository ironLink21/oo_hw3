using System.IO;

namespace AppLayer.Command
{
    public class CmdLoad : Command
    {
        private readonly string _filename;

        internal CmdLoad() { }
        internal CmdLoad(params object[] commandParameters)
        {
            if (commandParameters.Length > 0)
                _filename = commandParameters[0] as string;
        }

        public override void Execute()
        {
            StreamReader reader = new StreamReader(_filename);
            TargetDrawing?.LoadFromStream(reader.BaseStream);
            reader.Close();
        }

        public override void Undo()
        {
            // StreamReader reader = new StreamReader(_filename);
            // TargetDrawing?.LoadFromStream(reader.BaseStream);
            // reader.Close();
        }
    }
}
