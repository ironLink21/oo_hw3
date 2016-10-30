using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace AppLayer.Command
{
    public class Invoker
    {
        private Thread _worker;
        private readonly Stack<Command> _undoStack = new Stack<Command>();
        private readonly ConcurrentQueue<Command> _todoQueue = new ConcurrentQueue<Command>();
        private bool _keepGoing;
        private readonly AutoResetEvent _enqueueOccurred = new AutoResetEvent(false);

        public void Start()
        {
            _keepGoing = true;
            _worker = new Thread(Run);
            _worker.Start();
        }

        public void Stop()
        {
            _keepGoing = false;
        }

        public void EnqueueCmd(Command cmd)
        {
            if (cmd != null)
            {
				// Place the cmd into the _todoQueue
				_todoQueue.Enqueue(cmd);

				// Raise signal indicating that something was placed into the queue.  This wakes up the other thread,
				// if it is waiting for the something to be placed into the queue
                _enqueueOccurred.Set();
            }
        }

        public void Undo()
        {
			// Pop a command from the _undoStack, and call Undo on that command
			Command cmd = _undoStack.Pop();
			cmd.Undo();
        }

        private void Run()
        {
            while (_keepGoing)
            {
				Command cmd;
				// Try to get a command out of the queue.
				// If you got a command,
				if(_todoQueue.TryDequeue(out cmd))
				{
					// cmd = _todoQueue.Dequeue();
					// Execute the command.  Be sure that the execute method for the command
					// saves the necessary state information for an undo 
					// then Push it onto the undoStack.
					cmd.Execute();
					_undoStack.Push(cmd);
				}
				else 
				{
					// Else there was no command in the queue, execute the following statement, which
					// causes the thread to wait for up to 100 ms for something to be placed
					// into the queue
					_enqueueOccurred.WaitOne(100);
				}
            }
        }
    }
}