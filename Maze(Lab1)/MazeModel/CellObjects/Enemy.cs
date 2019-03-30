using System;
using Lab.LabirinthModel;

namespace Lab
{
    public class Enemy : Cell
    {
        public int health { get; set; }
        //public Boss(int x, int y) : base(x, y, 'O', ConsoleColor.Red) { }
        public Enemy(int x,int y) : base(x, y, 'O', ConsoleColor.Red) { isDead = false; health = 5; }
        public Enemy(int x,int y, int health_) : base(x, y, '0', ConsoleColor.Red) { health = health_; }
        public bool isDead;
        public override bool CanStep(Maze maze)
        {
            if (health > 0)
            {   if (Hero.GetHero.Attacks > 0)
                {
                    Hero.GetHero.Attacks--;
                    health--;
                }
            }
            if (health == 0)
            {
                isDead = true;
                Hero.GetHero.Experience += 5;
                maze.ReplaceWithGround(maze[X, Y]);
                return true;
            }
            return false;
        }
    }
}