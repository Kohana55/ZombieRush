using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ZombieRush
{
    public class Player
    {

        public int x;
        public int y;
        public int previousX;
        public int previousY;

        Map map;
        public int upperMapBound;

        ConsoleKeyInfo keypress;

        public int score = 0;
        public int moves = 0;

        public string tile = "\x1b[32m P \x1b[0m";

        public bool dead = false;
        public bool gameOver = false;

        public Player(bool gamO)
        {
            gameOver = gamO;
        }

        public void Spawn(Map _map)
        {
            map = _map;
            upperMapBound = _map.BoardSize;
            Random rng = new Random();
            x = rng.Next(0, upperMapBound);
            y = rng.Next(0, upperMapBound);
            previousX = x;
            previousY = y;
        }

        public void Move()
        {
            while (gameOver == false)
            {
                
                keypress = Console.ReadKey();

                // Grab players current position and save as previous position
                previousX = x;
                previousY = y;

                // Update players new position after keypress
                if (keypress.KeyChar == 'w')
                {
                    if (x != 0)
                    {
                        if (map.board[x - 1, y].HasWall)
                            continue;

                        x -= 1;
                    }
                    tile = "\x1b[32m ^ \x1b[0m";
                }

                if (keypress.KeyChar == 's')
                {
                    if (x != upperMapBound - 1)
                    {
                        if (map.board[x + 1, y].HasWall)
                            continue;

                        x += 1;
                    }
                    tile = "\x1b[32m V \x1b[0m";
                }

                if (keypress.KeyChar == 'a')
                {
                    if (y != 0)
                    {
                        if (map.board[x, y - 1].HasWall)
                            continue;

                        y -= 1;
                    }
                    tile = "\x1b[32m < \x1b[0m";
                }

                if (keypress.KeyChar == 'd')
                {
                    if (y != upperMapBound - 1)
                    {
                        if (map.board[x, y + 1].HasWall)
                            continue;

                        y += 1;
                    }
                    tile = "\x1b[32m > \x1b[0m";
                }

                if (x != previousX || y != previousY)
                {
                    moves++;
                }

                Thread.Sleep(10);
            }
        }
    }
}
