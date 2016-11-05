using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    /// <summary>
    /// CommandFactory
    /// 
    /// Creates standard commands, but can be specialized to create custom commands.  This class is the base
    /// class in a factory method pattern.
    /// </summary>
    public class CommandFactory
    {
        public Drawing TargetDrawing { get; set; }

        /// <summary>
        /// Create -- a factory method for standard commands 
        /// 
        /// This method can be overridden to generate different or custom commands.
        /// </summary>
        /// <param name="commandType">type of command to Create:
        ///             New
        ///             Add
        ///             Remove
        ///             Select
        ///             Deselect
        ///             Load
        ///             Save
        ///             MOVE
        ///             SCALE
        ///             DUPLICATE</param>
        /// <param name="commandParameters">An array of optional parameters whose sementics depedent on the command type
        ///     For new, no additional parameters needed
        ///     For add, 
        ///         [0]: Type       reference type for assembly containing the star type resource
        ///         [1]: string     star type -- a fully qualified resource name
        ///         [2]: Point      center location for the star, defaut = top left corner
        ///         [3]: float      scale factor</param>
        ///     For remove, no additional parameters needed
        ///     For select,
        ///         [0]: Point      Location at which a star could be selected
        ///     For deselect, no additional parameters needed
        ///     For load,
        ///         [0]: string     filename of file to load from  
        ///     For save,
        ///         [0]: string     filename of file to save to
        /// <returns></returns>
        public virtual Command Create(string commandType, params object[] commandParameters)
        {
            if (string.IsNullOrWhiteSpace(commandType)) return null;
            if ((commandType == "ADD" || commandType == "SELECT" || commandType == "LOAD" || commandType == "SAVE" || commandType == "MOVE" || commandType == "SCALE" || commandType == "DUPLICATE") && 
                (commandParameters == null || commandParameters.Length == 0)) return null;

            Command command=null;
            switch (commandType.Trim().ToUpper())
            {
                case "NEW":
                    command = new CmdNew();
                    break;
                case "ADD":
                    command = new CmdAdd(commandParameters);
                    break;
                case "REMOVE":
                    command = new CmdRemoveSelected();
                    break;
                case "SELECT":
                    command = new CmdSelect(commandParameters);
                    break;
                case "DESELECT":
                    command = new CmdDeselectAll();
                    break;
                case "LOAD":
                    command = new CmdLoad(commandParameters);
                    break;
                case "SAVE":
                    command = new CmdSave(commandParameters);
                    break;
                case "MOVE":
                    command = new CmdMove(commandParameters);
                    break;
                case "SCALE":
                    command = new CmdScale(commandParameters);
                    break;
                case "DUPLICATE":
                    command = new CmdDuplicate(commandParameters);
                    break;
            }

            if (command!=null)
            {
                command.TargetDrawing = TargetDrawing;
            }

            return command;
        }
    }
}

