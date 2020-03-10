using System;
using System.Reflection;
using ConsoleApplicationFramework.Library.Interfaces;

namespace ConsoleApplicationFramework.Library.Commands
{
    public class HelpCommand : ICommand
    {
        public string Name => "help";

        public string HelpText => "Prints out available commands.";

        public void Execute()
        {
            ConsoleBase.PrintHelp();
        }
    }
}
