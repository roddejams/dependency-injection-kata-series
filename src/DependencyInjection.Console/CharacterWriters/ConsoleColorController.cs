using System;
using DependencyInjection.Console.Entities;

namespace DependencyInjection.Console.CharacterWriters
{
    class ConsoleColorController : IColorController
    {
        public void SetColor(Square square)
        {
            var color = GetColor(square);
            System.Console.ForegroundColor = color;
            System.Console.BackgroundColor = color;
        }

        public void ResetColor()
        {
            System.Console.ResetColor();
        }

        private static ConsoleColor GetColor(Square square)
        {
            switch (square)
            {
                case Square.White:
                    return ConsoleColor.White;
                case Square.Black:
                    return ConsoleColor.Black;
                default:
                    throw new ArgumentException("Color not found for square type " + square);
            }
        }
    }
}