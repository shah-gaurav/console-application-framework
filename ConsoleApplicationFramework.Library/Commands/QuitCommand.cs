using System;
using ConsoleApplicationFramework.Library.Interfaces;

namespace ConsoleApplicationFramework.Library.Commands
{
    public class QuitCommand : ICommand
    {
        public string Name => "quit";

        public string HelpText => "Exits the application";

        public void Execute()
        {
            Console.WriteLine($"Command {Name} executed. Quiting the appication...");
            Environment.Exit(0);
        }
    }
}
