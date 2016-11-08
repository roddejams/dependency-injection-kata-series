using DependencyInjection.Console.CharacterWriters;
using DependencyInjection.Console.SquarePainters;
using NDesk.Options;

namespace DependencyInjection.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var useColors = false;
            var width = 25;
            var height = 15;
            var pattern = "circle";

            var optionSet = new OptionSet
            {
                {"c|colors", value => useColors = value != null},
                {"w|width=", value => width = int.Parse(value)},
                {"h|height=", value => height = int.Parse(value)},
                {"p|pattern=", value => pattern = value}
            };
            optionSet.Parse(args);
            
            var squarePainter = CreateSquarePainter(pattern);
            if (squarePainter == null)
            {
                System.Console.WriteLine("Please choose from one of the following patterns : \"circle\", \"oddeven\" or \"white\"");
                return;
            } 

            var patternGenerator = new PatternGenerator(squarePainter);

            ICharacterWriter asciiWriter = new AsciiWriter();
            var characterWriter = useColors ? new ColorWriter(asciiWriter) : asciiWriter;
            var patternWriter = new PatternWriter(characterWriter);

            var app = new PatternApp(patternGenerator, patternWriter);
            app.Run(width, height);
        }

        private static ISquarePainter CreateSquarePainter(string pattern)
        {
            switch (pattern)
            {
                case "circle":
                    return new CircleSquarePainter();
                case "oddeven":
                    return new OddEvenSquarePainter();
                case "white":
                    return new WhiteSquarePainter();
            }
            return null;
        }
    }
}
