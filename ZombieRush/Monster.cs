using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieRush
{
    class Monster
    {
        public string tile = "\x1b[31m M \x1b[0m";
        public int x;
        public int y;
        public int previousX;
        public int previousY;

        public int upperMapBound;

        public Monster(int BOARD_SIZE)
        {
            upperMapBound = BOARD_SIZE;
            Random rng = new Random();
            x = rng.Next(0, upperMapBound);
            y = rng.Next(0, upperMapBound);
            previousX = x;
            previousY = y;
        }

        public void Move()
        {
            Random rng = new Random();
            int moveDirection = rng.Next(0, 4);

            // Grab players current position and save as previous position
            previousX = x;
            previousY = y;

            // Update players new position after keypress
            if (moveDirection == 0)
            {
                if (x != 0)
                    x -= 1;
            }

            if (moveDirection == 1)
            {
                if (x != upperMapBound - 1)
                    x += 1;
            }

            if (moveDirection == 2)
            {
                if (y != 0)
                    y -= 1;
            }

            if (moveDirection == 3)
            {
                if (y != upperMapBound - 1)
                    y += 1;
            }
        }
    }
}
