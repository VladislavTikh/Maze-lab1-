using System;
using System.Collections.Generic;

namespace Lab
{
    class Application
    {
        static void Main(string[] args)
        {
            var generator = new MazeGenerator(8,8);
            var maze = generator.GetLabirinth();

            Console.WriteLine("Rules:");
            Console.WriteLine("Collect charges and kill the boss to win:");
            Console.WriteLine("Press R to change the maze");
            Console.WriteLine("Press ESC to exit");
            var hero = Hero.GetHero;
            ConsoleKeyInfo key;
            do
            {
                if (maze.isPassed)
                {
                    Console.Clear();
                    Console.WriteLine("Maze is passed");
                    break;
                } 
                key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        {
                            maze.MakeMove(Move.Up);
                            break;
                        }
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        {
                            maze.MakeMove(Move.Right);
                            break;
                        }
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        {
                            maze.MakeMove(Move.Down);
                            break;
                        }
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        {
                            maze.MakeMove(Move.Left);
                            break;
                        }
                    case ConsoleKey.R:
                        {
                            maze = generator.GetLabirinth();
                            break;
                        }
                }

                MazeView.Draw(maze);
            } while (key.Key != ConsoleKey.Escape);
            Console.Read();
        }
    }
}
