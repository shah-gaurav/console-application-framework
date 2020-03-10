using System.Collections.Generic;

namespace ConsoleApplicationFramework.Library.Interfaces
{
    public interface ICommand
    {
        string Name { get; }
        string HelpText { get; }

        void Execute();
    }
}