using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieRush
{
    public class Cell
    {
        /// <summary>
        /// Const values to represent each of the tiles
        /// states.
        /// </summary>
        private const string EMPTY_TILE = "   ";
        private const string MONSTER_TILE = "\x1b[31m M \x1b[0m";
        private const string ITEM_TILE = "\x1b[33m * \x1b[0m";
        private const string WALL_TILE = " X ";


        public string tile = EMPTY_TILE;
       
        public bool HasItem
        { get
            { return hasItem; }
            set
            {
                hasItem = value;
                if (hasItem == true)
                    tile = ITEM_TILE;
                else
                    tile = EMPTY_TILE;
            }
        }
        private bool hasItem = false;

        public bool HasMonster
        {
            get
            {
                return hasMonster;
            }
            set
            {
                if (value == true)
                    tile = MONSTER_TILE;
                else if (value == false && hasItem == true)
                    tile = ITEM_TILE;
                else
                    tile = EMPTY_TILE;

                hasMonster = value;
            }
        }
        private bool hasMonster = false;

        public bool HasWall
        {
            get
            {
                return hasWall;
            }
            set
            {
                if (value == true)
                {
                    tile = WALL_TILE;
                    hasWall = true;
                }
            }
        }
        private bool hasWall = false;

    }
}
