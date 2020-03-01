using System;
using System.Collections.Generic;
using System.Text;

namespace TestDataBuilderGenerator
{
    public static class InputHandler
    {
        public static int GetChoice(int min, int max)
        {

            while (true)
            {
                Console.WriteLine($"Please enter a number between {min} and {max}\n");
                if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int result))
                {
                    if (result >= min && result <= max)
                    {
                        return result;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number");
                }
            }
        }
        public static string GetPastedData()
        {
            return TextCopy.Clipboard.GetText();

        }
    }
}
