using Autofac; 
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
            var pattern = "sdkfjhfskjdfhk";

            var optionSet = new OptionSet
                {
                    {"c|colors", value => useColors = value != null},
                    {"w|width=", value => width = int.Parse(value)},
                    {"h|height=", value => height = int.Parse(value)},
                    {"p|pattern=", value => pattern = value}
                };
            optionSet.Parse(args);

            var builder = new ContainerBuilder();

            builder.RegisterModule(new PatternAppModule
            {
                Pattern = pattern,
                UseColors =  useColors
            });

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<PatternApp>();
                app.Run(width, height);
            }
        }
    }
}
