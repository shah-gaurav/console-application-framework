using System;
using ConsoleApplicationFramework.Library.Annotations;
using ConsoleApplicationFramework.Library.Interfaces;

namespace ConsoleApplicationFramework.Sample
{
    public class SampleCommand : ICommand
    {
        public string Name => "sayhi";

        public string HelpText => "Says hello to the person!";

        [Argument("Annonymous")]
        public string UserName { get; set; }

        public void Execute()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Hi {UserName}! Welcome to ConsoleApplicationFramework");
            Console.ResetColor();
        }
    }
}
