using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieRush
{
    public class Item
    {

        public int x;
        public int y;

        public string tile = " * ";

        public Item(int X, int Y)
        {
            x = X;
            y = Y;
        }

    }
}
