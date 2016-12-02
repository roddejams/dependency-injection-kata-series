using System;
using Autofac.Features.Indexed;
using DependencyInjection.Console.Entities;
using DependencyInjection.Console.SquarePainters;

namespace DependencyInjection.Console
{
    internal class PatternGenerator
    {
        private readonly ISquarePainter _squarePainter;

        public PatternGenerator(IIndex<string, ISquarePainter> squarePainter, string pattern)
        {
            if (!squarePainter.TryGetValue(pattern, out _squarePainter))
            {
                throw new ArgumentException($"Pattern '{pattern}' not found!");
            }
        }

        public Pattern Generate(int width, int height)
        {
            var pattern = new Pattern(width, height);
            var squares = pattern.Squares;

            for (var i = 0; i < width; ++i)
            {
                for (var j = 0; j < height; ++j)
                {
                    squares[j, i] = _squarePainter.PaintSquare(width, height, i, j);
                }
            }

            return pattern;
        }
    }
}