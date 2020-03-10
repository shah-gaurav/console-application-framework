using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConsoleApplicationFramework.Library.Annotations;
using ConsoleApplicationFramework.Library.Interfaces;

namespace ConsoleApplicationFramework.Library
{
    public static class ConsoleBase
    {
        private static List<ICommand> Commands = new List<ICommand>();

        static ConsoleBase()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var currentType in assembly.DefinedTypes)
                {
                    if (currentType.ImplementedInterfaces.Contains(typeof(ICommand)) && !currentType.IsAbstract)
                    {
                        var command = (ICommand)Activator.CreateInstance(currentType.AsType());
                        Commands.Add(command);
                    }
                }
            }
        }
        public static void Run()
        {
            PrintHelp();
            while (true)
            {
                var command = GetCommand();
                try
                {
                    GetArguments(command);
                    ExecuteCommand(command);
                    Console.WriteLine($"Command {command.Name} executed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Command {command.Name} Error: {ex.Message}.");
                    Console.WriteLine(ex);
                }
                Console.WriteLine("--------------------------------------------------");
            }
        }

        private static void GetArguments(ICommand command)
        {
            var properties = command.GetType().GetProperties().Where(
                p => Attribute.IsDefined(p, typeof(ArgumentAttribute)));
            if (properties.Count() > 0)
            {
                Console.WriteLine("This command requires some arguments to execute. Please provide them below:");
                foreach (var property in properties)
                {
                    // Get instance of the attribute.
                    ArgumentAttribute argumentAttribute =
                        (ArgumentAttribute)Attribute.GetCustomAttribute(property, typeof(ArgumentAttribute));
                    var defaultValue = argumentAttribute.DefaultValue;

                    Console.Write($"  {property.Name} [{defaultValue}] > ");
                    var propertyValue = Console.ReadLine();
                    propertyValue = string.IsNullOrWhiteSpace(propertyValue) ? defaultValue : propertyValue.Trim();
                    if (property.PropertyType.IsEnum)
                    {
                        var enumValue = Enum.Parse(property.PropertyType, propertyValue);
                        property.SetValue(command, enumValue, null);
                    }
                    else
                    {
                        property.SetValue(command, Convert.ChangeType(propertyValue, property.PropertyType), null);
                    }
                }
            }

        }

        private static void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }

        private static ICommand GetCommand()
        {
            while (true)
            {
                Console.Write("Command > ");
                var enteredCommand = Console.ReadLine();
                if (Commands.Any(c => c.Name.ToLower() == enteredCommand.ToLower()))
                {
                    return Commands.First(c => c.Name.ToLower() == enteredCommand.ToLower());
                }
                else
                {
                    Console.WriteLine("Command not found. Try again!");
                }
            }
        }

        public static void PrintHelp()
        {
            Console.WriteLine("Available Commands: ");
            foreach (var command in Commands)
            {
                Console.WriteLine($"{command.Name}: {command.HelpText}");
            }
        }
    }
}
