using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieRush
{
    public class Item
    {

        public int x;
        public int y;

        public string tile = "\x1b[42m \x1b[33m* \x1b[0m";

        public Item(int X, int Y)
        {
            x = X;
            y = Y;
        }

    }
}
