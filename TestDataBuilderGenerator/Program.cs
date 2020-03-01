using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDataBuilderGenerator
{

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-------------TEST DATA BUILDER V 1.0--------------\n\n");
                Console.WriteLine("Options\n1. Get Builder Base Class\n2 Convert Data Class into builder class.\n3 Exit\n\n");
                Console.WriteLine("--------------------------------------------------\n\n");
                int choice = InputHandler.GetChoice(1, 3);
                if (choice == 1)
                {
                    HandleBaseClassRequest();
                }
                else if (choice == 2)
                {
                    HandleDataConversion();
                }
                else
                {
                    break;
                }
            }

        }

        private static void HandleDataConversion()
        {
            Console.Clear();
            Console.WriteLine("Press enter to paste data");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(@"/\/\/\/\/\BEGIN PASTED DATA/\/\/\/\/\/\" + "\n\n");
            string data = InputHandler.GetPastedData();
            Console.WriteLine(data);
            Console.WriteLine("\n\n" + @"/\/\/\/\/\END PASTED DATA/\/\/\/\/\/\" + "\n\n");
           
            Console.WriteLine("\n\nPress enter to confirm, or press any other key to return to menu");
            ConsoleKey key = Console.ReadKey().Key;
            if (key != ConsoleKey.Enter)
            {
                return;
            }
            try
            {
                ProcessData(data);
                Console.WriteLine("\n\nText copied to clipboard!\n");

            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Was unable to process data. Please make sure data is valid and try again.\nData passed into program should be a simple POCO with only public properties or fields");
            }
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }

        private static void HandleBaseClassRequest()
        {
            Display.DrawCopySectionOpener("BUILDER BASE CLASS");
            string baseClass = Display.DrawBuilderBaseClass();
            Display.DrawCopySectionCloser("BUILDER BASE CLASS");
            TextCopy.Clipboard.SetText(baseClass);
            Console.WriteLine("\n\nText copied to clipboard!\n");
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }

        private static void ProcessData(string dataClass)
        {
            List<Property> props = ConvertClassToProperties(ref dataClass);
            Property classInfo = props.First(x => x.Type == "class");
            string builderName = classInfo.Name + "Builder";
            BuilderClassProperties builderClassProperties = new BuilderClassProperties
            {
                BuilderType = builderName,
                DataType = classInfo.Name,
                PrivateFields = props.Where(x => x.Type != "class").ToList()
            };
            string builderClass = ClassBuilder.BuildClass(builderClassProperties);
            Console.Clear();
            Display.DrawCopySectionOpener(builderClassProperties.BuilderType.ToUpper());
            Console.WriteLine(builderClass);
            Display.DrawCopySectionCloser(builderClassProperties.BuilderType.ToUpper());
            TextCopy.Clipboard.SetText(builderClass);
        }

        private static List<Property> ConvertClassToProperties(ref string dataClass)
        {
            var props = new List<Property>();
            dataClass = dataClass.Trim();
            var lines = dataClass.Split(Environment.NewLine);
            foreach (var line in lines)
            {
                var prop = ConvertLineToProperty(line);
                if (prop != null)
                {
                    props.Add(prop);
                }

            }

            return props;
        }
        private static Property ConvertLineToProperty(string line)
        {
            line = line.Trim();
            Property prop = new Property();
            string[] words = line.Split(" ");
            if (words.Length < 3)
            {
                return null;
            }

            prop.Modifier = words[0];
            if (prop.Modifier != "public")
            {
                throw new Exception();
            }
            prop.Type = words[1];
            prop.Name = words[2];



            return prop;
        }
    }
}
