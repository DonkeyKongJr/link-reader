using System;
using Writer;

namespace Writer
{
    public class OverwriteSamePositionWriter : IOverwriteSamePositionWriter
    {
        public void OverwriteSamePosition(string text)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine(text);
        }
    }
}