using System;
using Autofac;
using DependencyInjection.Console.CharacterWriters;
using DependencyInjection.Console.SquarePainters;

namespace DependencyInjection.Console
{
    class PatternAppModule : Module
    {
        public bool UseColors { get; set; }
        public string Pattern { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new AsciiWriter()).As<ICharacterWriter>().As<AsciiWriter>();
            if (UseColors) builder.Register(c => new ColorWriter(c.Resolve<AsciiWriter>())).As<ICharacterWriter>();
            builder.Register(c => new PatternWriter(c.Resolve<ICharacterWriter>())).As<PatternWriter>();
            builder.Register(c => GetSquarePainter(Pattern)).As<ISquarePainter>();
            builder.Register(c => new PatternGenerator(c.Resolve<ISquarePainter>())).As<PatternGenerator>();
            builder.Register(c => new PatternApp(c.Resolve<PatternWriter>(), c.Resolve<PatternGenerator>())).As<PatternApp>();
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
