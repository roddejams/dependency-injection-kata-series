using DependencyInjection.Console.Entities;

namespace DependencyInjection.Console.CharacterWriters
{
    internal interface IColorController
    {
        void SetColor(Square square);
        void ResetColor();
    }
}