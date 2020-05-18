using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ZombieRush
{
    class Player
    {

        public int x;
        public int y;
        public int previousX;
        public int previousY;

        public int upperMapBound;

        ConsoleKeyInfo keypress;

        public int score = 0;
        public int moves = 0;

        public string tile = " \x1b[32mP\x1b[0m ";

        public bool dead = false;
        public bool gameOver = false;

        public Player(bool gamO)
        {
            gameOver = gamO;
        }

        public void Spawn()
        {
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
                        x -= 1;
                    tile = " \x1b[32m^\x1b[0m ";
                }

                if (keypress.KeyChar == 's')
                {
                    if (x != upperMapBound - 1)
                        x += 1;
                    tile = " \x1b[32mV\x1b[0m ";
                }

                if (keypress.KeyChar == 'a')
                {
                    if (y != 0)
                        y -= 1;
                    tile = " \x1b[32m<\x1b[0m ";
                }

                if (keypress.KeyChar == 'd')
                {
                    if (y != upperMapBound - 1)
                        y += 1;
                    tile = " \x1b[32m>\x1b[0m ";
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
