using System;
using System.IO;
using DependencyInjection.Console.Entities;

namespace DependencyInjection.Console.CharacterWriters
{
    internal class AsciiWriter : ICharacterWriter
    {
        private readonly TextWriter _output;

        public AsciiWriter(TextWriter textWriter)
        {
            _output = textWriter;
        }

        public void Write(Square square)
        {
            var character = GetCharacter(square);
            _output.Write(character);
        }

        public void WriteLine()
        {
            _output.WriteLine();
        }

        private static char GetCharacter(Square square)
        {
            switch (square)
            {
                case Square.White:
                    return '.';
                case Square.Black:
                    return '#';
                default:
                    throw new ArgumentException("Char not found for square type " + square);
            }
        }
    }
}