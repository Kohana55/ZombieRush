using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieRush
{
    class Map
    {
        public int BoardSize { get
            {
                return BOARD_SIZE;
            }
        }
        private const int BOARD_SIZE = 16;
        private const int NUM_ITEMS = 20;
        private const int NUM_MONSTERS = 10;
        public bool gameOver = false;

        public Cell[,] board = new Cell[BOARD_SIZE, BOARD_SIZE];
        Player player;
        List<Monster> monsters = new List<Monster>();
        public List<Item> items = new List<Item>();
        Random rng = new Random();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_player"></param>
        /// <param name="_monsters"></param>
        public Map(Player _player, List<Monster> _monsters, List<Item> _items, bool gamO)
        {
            gameOver = gamO;

            // Construct the Cells of each board location
            GenerateBoard();

            // Setup Player on the map
            player = _player;
            player.upperMapBound = BOARD_SIZE;
            player.Spawn();

            // Setup Monsters on the map
            monsters = _monsters;

            // Create item list and add to map
            items = _items;
            for (int i = 0; i < NUM_ITEMS; i++)
            {
                items.Add(new Item(rng.Next(0, BOARD_SIZE), rng.Next(0, BOARD_SIZE)));
                board[items[i].x, items[i].y].tile = " * ";
                board[items[i].x, items[i].y].hasItem = true;
            }

            // Create monster list and add to map
            for (int i = 0; i < NUM_MONSTERS; i++)
            {
                monsters.Add(new Monster(this));
                board[monsters[i].x, monsters[i].y].tile = " M ";
            }
        }

        public void RespawnItems()
        {
            items.Clear();
            for (int i = 0; i < NUM_ITEMS; i++)
            {
                items.Add(new Item(rng.Next(0, BOARD_SIZE), rng.Next(0, BOARD_SIZE)));
                board[items[i].x, items[i].y].tile = items[i].tile;
                board[items[i].x, items[i].y].hasItem = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void GenerateBoard()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    board[i, j] = new Cell
                    {
                        tile = "   "
                    };
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            // Update Player
            board[player.previousX, player.previousY].tile = "   ";
            board[player.x, player.y].tile = player.tile;

            // Update Monsters
            for (int i=0; i < monsters.Count; i++)
            {
                if (board[monsters[i].previousX, monsters[i].previousY].hasItem)
                    board[monsters[i].previousX, monsters[i].previousY].tile = " * ";
                else
                    board[monsters[i].previousX, monsters[i].previousY].tile = "   ";
                board[monsters[i].x, monsters[i].y].tile = " M ";
            }

            // Check for collisions
            CheckForCollisions();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="items"></param>
        public void CheckForCollisions()
        {
            // Collision control
            int toBeDeleted = 0;
            bool collisionDetected = false;

            // Collision with Item
            for (int i = 0; i < items.Count; i++)
            {
                if (player.x == items[i].x && player.y == items[i].y)
                {
                    player.score++;
                    board[items[i].x, items[i].y].hasItem = false;
                    toBeDeleted = i;
                    collisionDetected = true;
                    gameOver = true;
                }
            }

            if (collisionDetected)
                items.RemoveAt(toBeDeleted);

            // Collision with Monster
            for (int i=0;i<monsters.Count;i++)
            {
                if (player.x == monsters[i].x && player.y == monsters[i].y)
                {
                    player.dead = true;
                    board[player.x, player.y].tile = " # ";
                }
            }
        }

    }
}
