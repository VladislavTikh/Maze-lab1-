using Lab.LabirinthModel;
using System;

namespace Lab
{
    public class Charge : Cell
    {
        public const ConsoleColor CoinColor = ConsoleColor.Yellow;

        public Charge(int x, int y) : base(x, y, '*', CoinColor) { }

        public override bool CanStep(Maze maze)
        {
            Hero.GetHero.Attacks++;
            maze.ReplaceWithGround(maze[X,Y]);
            return true;
        }
    }
}