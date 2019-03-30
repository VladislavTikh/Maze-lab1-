using System;
using Lab.LabirinthModel;

namespace Lab
{
    public class Wall : Cell
    {
        public Wall(int x, int y) : base(x, y, '#', ConsoleColor.Gray) { }

        public override bool CanStep(Maze maze)
        {
            return false;
        }
    }
}