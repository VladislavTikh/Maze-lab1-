using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.LabirinthModel
{
    public abstract class Cell
    {
        public Cell(int x, int y, char symbol, ConsoleColor? color = null)
        {
            X = x;
            Y = y;
            Symbol = symbol;
            Color = color;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }
        public ConsoleColor? Color { get; set; }
        public abstract bool CanStep(Maze labirinth);
    }
}
