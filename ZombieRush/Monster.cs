using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieRush
{
    public class Monster
    {
        public string tile = "\x1b[31m M \x1b[0m";
        public int x;
        public int y;
        public int previousX;
        public int previousY;

        Map map;
        public int upperMapBound;

        public Monster(Map _map)
        {
            map = _map;
            upperMapBound = map.BoardSize;
            Spawn();
        }

        public void Spawn()
        {
            Random rng = new Random();       
            while (true)
            {
                x = rng.Next(0, upperMapBound);
                y = rng.Next(0, upperMapBound);
                if (map.board[x, y].HasWall == false) break;
            }

            previousX = x;
            previousY = y;
        }

        public void Move()
        {
            while (true)
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
                    {
                        if (map.board[x - 1, y].HasWall)
                            continue;

                        if (map.board[x - 1, y].HasMonster)
                            break;

                        x -= 1;
                        break;
                    }
                }

                if (moveDirection == 1)
                {
                    if (x != upperMapBound - 1)
                    {
                        if (map.board[x + 1, y].HasWall)
                            continue;

                        if (map.board[x + 1, y].HasMonster)
                            break;

                        x += 1;
                        break;
                    }
                }

                if (moveDirection == 2)
                {
                    if (y != 0)
                    {
                        if (map.board[x, y - 1].HasWall)
                            continue;

                        if (map.board[x, y - 1].HasMonster)
                            break;

                        y -= 1;
                        break;
                    }
                }

                if (moveDirection == 3)
                {
                    if (y != upperMapBound - 1)
                    {
                        if (map.board[x, y + 1].HasWall)
                            continue;

                        if (map.board[x, y + 1].HasMonster)
                            break;

                        y += 1;
                        break;
                    }
                }
            }
            map.board[previousX, previousY].HasMonster = false;
            map.board[x, y].HasMonster = true;
        }
    }
}
