using System;
using System.IO;
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
            builder.RegisterInstance(System.Console.Out).As<TextWriter>();
            builder.Register(c => new AsciiWriter(c.Resolve<TextWriter>())).Keyed<ICharacterWriter>(false);
            builder.Register(c => new ConsoleColorController()).As<IColorController>();
            builder.Register(c => new ColorWriter(c.ResolveKeyed<ICharacterWriter>(false), c.Resolve<IColorController>())).Keyed<ICharacterWriter>(true);

            builder.Register(c => new PatternWriter(c.ResolveKeyed<ICharacterWriter>(UseColors))).As<PatternWriter>();
            builder.Register(c => new CircleSquarePainter()).Named<ISquarePainter>("circle");
            builder.Register(c => new OddEvenSquarePainter()).Named<ISquarePainter>("oddeven");
            builder.Register(c => new WhiteSquarePainter()).Named<ISquarePainter>("white");
            builder.RegisterType<PatternGenerator>().WithParameter("pattern", Pattern).As<PatternGenerator>();

            builder.Register(c => new PatternApp(c.Resolve<PatternWriter>(), c.Resolve<PatternGenerator>())).As<PatternApp>();
        }
    }
}
