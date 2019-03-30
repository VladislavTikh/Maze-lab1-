using System;
using System.Text;

namespace Lab
{
    public class MazeView
    {
        public static void Draw(Maze lab)
        {
            Console.Clear();

            var hero = Hero.GetHero;
            Console.Write("Hero charges: ");
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = Charge.CoinColor;
            Console.Write(hero.Attacks);
            Console.WriteLine();
            Console.ForegroundColor = oldColor;
            Console.Write("Hero experience: ");
            Console.Write(hero.Experience);
            DrawWalls(lab.Width+2);
            for (int y = 0; y < lab.Height; y++) {
                Console.WriteLine();
                Console.Write("#");
                for (int x = 0; x < lab.Width; x++) {
                    var currentCell = lab[x, y];
                    if (hero.X == x && hero.Y == y)
                        Console.Write(hero.Symbol);
                     else if (lab._boss.X == x && lab._boss.Y == y)
                    {
                        oldColor = Console.ForegroundColor;
                        Console.ForegroundColor = (ConsoleColor)lab._boss.Color;
                        Console.Write(lab._boss.Symbol);
                        Console.ForegroundColor = oldColor;
                    }
                    else
                    {
                        oldColor = Console.ForegroundColor;
                        Console.ForegroundColor = currentCell.Color ?? oldColor;
                        Console.Write(currentCell.Symbol);
                        Console.ForegroundColor = oldColor;
                    }
                }
                Console.Write("#");
            }
            DrawWalls(lab.Width + 2);
        }
        private static void DrawWalls(int size)
        {
            Console.WriteLine();
            for (var i = 0; i < size; i++)
            {
                Console.Write("#");
            }
        }
    }
}
