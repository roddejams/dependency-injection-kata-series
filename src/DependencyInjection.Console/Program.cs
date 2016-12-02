using System;
using Autofac;
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

            var builder = new ContainerBuilder();

            builder.Register(c => new AsciiWriter()).As<ICharacterWriter>().As<AsciiWriter>();
            if (useColors) builder.Register(c => new ColorWriter(c.Resolve<AsciiWriter>())).As<ICharacterWriter>();
            builder.Register(c => new PatternWriter(c.Resolve<ICharacterWriter>())).As<PatternWriter>();
            builder.Register(c => GetSquarePainter(pattern)).As<ISquarePainter>();
            builder.Register(c => new PatternGenerator(c.Resolve<ISquarePainter>())).As<PatternGenerator>();
            builder.Register(c => new PatternApp(c.Resolve<PatternWriter>(), c.Resolve<PatternGenerator>())).As<PatternApp>();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<PatternApp>();
                app.Run(width, height);
            }
        }

        private static ISquarePainter GetSquarePainter(string pattern)
        {
            switch (pattern)
            {
                case "circle":
                    return new CircleSquarePainter();
                case "oddeven":
                    return new OddEvenSquarePainter();
                case "white":
                    return new WhiteSquarePainter();
                default:
                    throw new ArgumentException($"Pattern '{pattern}' not found!");
            }
        }
    }
}
