using System;
using System.Collections.Generic;
using System.Text;

namespace TestDataBuilderGenerator
{
    public static class Display
    {
        public static void DrawCopySectionOpener(string title)
        {
            Console.WriteLine($"------------------{title} START-----------------\n");
            Console.WriteLine(@"\/\/\/\/\//\/\/\/\--COPY BELOW--/\/\/\/\/\/\/\/\/\/\/\" + "\n\n\n");
        }
        public static void DrawCopySectionCloser(string title)
        {
            Console.WriteLine("\n\n\n" + @"\/\/\/\/\//\/\/\/\--COPY ABOVE--/\/\/\/\/\/\/\/\/\/\/\");
            Console.WriteLine($"\n------------------{title} END ------------------\n");
        }
        public static string DrawBuilderBaseClass()
        {
            string baseClass = "public abstract class Builder<T>" + Environment.NewLine +
            "    {" + Environment.NewLine +
            "        public abstract T Build();" + Environment.NewLine +
            "        public static implicit operator T(Builder<T> builder)" + Environment.NewLine +
            "        {" + Environment.NewLine +
            "            return builder.Build();" + Environment.NewLine +
            "        }" + Environment.NewLine +
            "    }";
            Console.WriteLine(baseClass + "\n\n");
            return baseClass;
        }
    }
}
