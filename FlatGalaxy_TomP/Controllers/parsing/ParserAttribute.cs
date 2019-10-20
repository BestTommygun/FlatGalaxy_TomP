using System;

[System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
sealed class ParserAttribute : Attribute
{
    public string Name { get; }

    // This is a positional argument
    public ParserAttribute(string name)
    {
        this.Name = name;
    }
}
