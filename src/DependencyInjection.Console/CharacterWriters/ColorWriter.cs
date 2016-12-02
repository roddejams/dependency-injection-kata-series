using System;
using DependencyInjection.Console.Entities;

namespace DependencyInjection.Console.CharacterWriters
{
    internal class ColorWriter : ICharacterWriter
    {
        private readonly ICharacterWriter _innerWriter;
        private readonly IColorController _colorController;

        public ColorWriter(ICharacterWriter innerWriter, IColorController colorController)
        {
            _innerWriter = innerWriter;
            _colorController = colorController;
        }

        public void Write(Square square)
        {
            _colorController.SetColor(square);
            _innerWriter.Write(square);
            _colorController.ResetColor();
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

        public void WriteLine()
        {
            _innerWriter.WriteLine();
        }
    }
}
