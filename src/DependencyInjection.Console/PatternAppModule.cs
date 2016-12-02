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
            builder.Register(c => new CircleSquarePainter()).Named<ISquarePainter>("circle");
            builder.Register(c => new OddEvenSquarePainter()).Named<ISquarePainter>("oddeven");
            builder.Register(c => new WhiteSquarePainter()).Named<ISquarePainter>("white");
            builder.Register(ResolvePatternGenerator).As<PatternGenerator>();

            builder.Register(c => new PatternApp(c.Resolve<PatternWriter>(), c.Resolve<PatternGenerator>())).As<PatternApp>();
        }

        private PatternGenerator ResolvePatternGenerator(IComponentContext context)
        {
            var squarePainter = context.ResolveOptionalNamed<ISquarePainter>(Pattern);

            if (squarePainter == null)
            {
                throw new ArgumentException($"Pattern '{Pattern}' not found!");
            }

            return new PatternGenerator(squarePainter);
        }
    }
}
