using System;

namespace ConsoleApplicationFramework.Library.Annotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class ArgumentAttribute : Attribute
    {
        public string DefaultValue { get; set; }
        public ArgumentAttribute(string defaultValue)
        {
            this.DefaultValue = defaultValue;
        }
    }
}
