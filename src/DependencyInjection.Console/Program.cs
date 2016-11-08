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
            var pattern = "circle"; // TODO: Hook this up

            var optionSet = new OptionSet
            {
                {"c|colors", value => useColors = value != null},
                {"w|width=", value => width = int.Parse(value)},
                {"h|height=", value => height = int.Parse(value)},
                {"p|pattern=", value => pattern = value}
            };
            optionSet.Parse(args);

            ISquarePainter squarePainter = new CircleSquarePainter();
            if (pattern.Equals("oddeven"))
            {
                squarePainter = new OddEvenSquarePainter();
            }

            if (pattern.Equals("white"))
            {
                squarePainter = new WhiteSquarePainter();
            }
            var patternGenerator = new PatternGenerator(squarePainter);

            ICharacterWriter asciiWriter = new AsciiWriter();
            var characterWriter = useColors ? new ColorWriter(asciiWriter) : asciiWriter;
            var patternWriter = new PatternWriter(characterWriter);

            var app = new PatternApp(patternGenerator, patternWriter);
            app.Run(width, height);
        }
    }
}
