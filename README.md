# Creator
Seth Bertlshofer
10/27/16
Assign3

# Working/Implemented
command
invoker for command pattern
flyweight
undo patterns

10 - different commands
2-3 - tests for each command (note: the tests work however there is a bug that I can't seem to fix, they run concurrently but they add upon eachother) 

# Not Working/Implemented
GUI

# Docs
UML diagram can be found in "./docs/hw3_class_uml.vpp" (visual paradigm file)
// Action Diagram can be found in "./docs/hw3_class_interaction.vpp" (visual paradigm file)
// State Diagram can be found in "./docs/hw3_class_state.vpp" (visual paradigm file)

# Dependencies
mono
dotnet
- I created this project on Linux use the .net & mono frameworks

# Note 
You run this program via cmdline.  if you install the dotnet framework for your OS all you need to do to run tests
is go to ./hw3/test/AppLayerTesting/ then run "dotnet test"
I don't have an actual program created to run everything because it's a console application.  The tests do run and 
theoretically should pass with how I have them setup minus the issue noted above.
