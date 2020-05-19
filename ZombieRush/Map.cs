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
        private const int BOARD_SIZE = 28;
        private const int NUM_ITEMS = 20;
        private const int NUM_MONSTERS = 10;
        public bool gameOver = false;

        public Cell[,] board = new Cell[BOARD_SIZE, BOARD_SIZE];
        Player player;
        List<Monster> monsters = new List<Monster>();
        public List<Item> items = new List<Item>();
        Timer timer;
        Random rng = new Random();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_player"></param>
        /// <param name="_monsters"></param>
        /// <param name="_items"></param>
        /// <param name="gamO"></param>
        public Map(Player _player, List<Monster> _monsters, List<Item> _items,Timer _timer, bool gamO)
        {
            gameOver = gamO;
            timer = _timer;

            // Construct the Cells of each board location
            GenerateBoard();

            // Setup Player on the map
            player = _player;
            player.Spawn(BOARD_SIZE);

            // Create item list and add to map
            items = _items;
            SpawnItems();

            // Create monster list and add to map
            monsters = _monsters;
            SpawnMonsters();
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
                    board[i, j] = new Cell();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SpawnMonsters()
        {
            for (int i = 0; i < NUM_MONSTERS; i++)
            {
                monsters.Add(new Monster(BOARD_SIZE));
                board[monsters[i].x, monsters[i].y].HasMonster = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SpawnItems()
        {
            items.Clear();
            for (int i = 0; i < NUM_ITEMS; i++)
            {
                items.Add(new Item(rng.Next(0, BOARD_SIZE), rng.Next(0, BOARD_SIZE)));
                board[items[i].x, items[i].y].tile = items[i].tile;
                board[items[i].x, items[i].y].HasItem = true;
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

            //// Update Monsters
            for (int i = 0; i < monsters.Count; i++)
            {
                board[monsters[i].previousX, monsters[i].previousY].HasMonster = false;
                board[monsters[i].x, monsters[i].y].HasMonster = true;
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
                    board[items[i].x, items[i].y].HasItem = false;
                    toBeDeleted = i;
                    collisionDetected = true;
                    timer.TimeRemaining += 1;
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
